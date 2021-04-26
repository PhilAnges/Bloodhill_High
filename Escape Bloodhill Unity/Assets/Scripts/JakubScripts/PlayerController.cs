﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     public float collisionDistance = 2f;
     public float collisionScale = 0.5f;

     */
    [HideInInspector]
    public PlayerState currentState;
    [HideInInspector]
    public FirstPersonCamera cam;
    [HideInInspector]
    public Transform flashlight;
    [HideInInspector]
    public Light[] lights;
    [HideInInspector]
    public Rigidbody rigbod;
    public GameObject camPrefab;
    public GameObject screenFadePrefab;
    [HideInInspector]
    public PlayerHealth hp;
    [HideInInspector]
    public ItemPickup itemScript;
    public GameController gameController;

    [SerializeField]
    private float stamina = 100f;
    [HideInInspector]
    public Transform ghost;
    private float ghostDistance;
    private CapsuleCollider myCollider;
    private AudioSource heart;

    [Range(100f, 1000f)]
    public float moveSpeed = 250f;
    public float runSpeedMultiplier = 2f;
    public float runStaminaCost = 10f;
    public float crouchSpeedMultiplier = 0.5f;
    public float staminaRegenRate = 5f;
    public float staminaRegenMultiplier = 0.5f;

    [HideInInspector]
    public float ogMoveSpeed;
    [HideInInspector]
    public float ogRegenRate;
    [HideInInspector]
    public float ogstepInterval;
    [HideInInspector]
    public float flickTimer;
      
    public float stepInterval = 2f;    
    public float runInterval = 0.2f;
    public float crouchInterval = 0.5f;
    public float runBobIntensity = 0.2f;
    public float crouchBobIntensity = 0.05f;
    public float walkSwayIntensity = 0.2f;
    public float runSwayIntensity = 0.2f;
    public float crouchSwayIntensity = 0.1f;
 
    public int adrenalineLevel = 0;
    public float lvlOneThreshold;
    public float lvlTwoThreshold;
    public float lvlThreeThreshold;

    [HideInInspector]
    public int noiseLevel = 0;
    [HideInInspector]
    public bool isHidden = false;
    [HideInInspector]
    public bool noGhost = false;
    [HideInInspector]
    public AIController ghostScript;
    [HideInInspector]
    public bool isBeingChased = false;
    [HideInInspector]
    public bool running = false;
    //[HideInInspector]
    public bool airborn = false;
    [HideInInspector]
    public bool flashLightOn = false;
    [HideInInspector]
    public bool isCrouching = false;
    //[HideInInspector]
    public bool hasTriggerItem = false;

    public int musicState;
    public bool tension = false;
    public bool safety = false;

    public AudioSource[] footsteps;
    public AudioSource breath;
    public AudioSource breath2;
    private bool breathing = false;
    private int groundMask;

    private float[] ogStepVolume;
    private float breathVolume;
    private bool breathPause = false;
    public float fadeInSpeed = 0.1f;
    private bool cursorDeactivated = false;

    public bool suspendControls = false;


    

    private void Awake()
    {
        Instantiate(screenFadePrefab);
        AudioListener.volume = 0f;
        StartCoroutine("AudioFadeIn");
        cam = Instantiate(camPrefab, transform.position, transform.rotation).GetComponent<FirstPersonCamera>();
        cam.SetParent(this);
        ogMoveSpeed = 100f;
        ogRegenRate = 30f;
        ogstepInterval = 0.35f;
        rigbod = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();
        heart = GetComponentInChildren<AudioSource>();
        hp = GetComponent<PlayerHealth>();
        itemScript = GetComponent<ItemPickup>();
        groundMask = LayerMask.GetMask("Ground");
        if (FindGhost() == null)
        {
            noGhost = true;
        }
        else
        {
            ghost = FindGhost().transform;
            ghostScript = ghost.GetComponent<AIController>();
        }
        breathVolume = breath.volume;
        GetComponent<PlayerHealth>().gameOverMenu = GameObject.Find("John's Programming Box").transform.Find("GameOver").gameObject;
        GameObject.Find("John's Programming Box").transform.Find("Safe Room").GetComponent<SafeRoom>().player = this.gameObject;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SetState(new PlayerIdle(this));
        StartCoroutine("RandomFlicker");
        Cursor.visible = false;
        suspendControls = false;
    }

    void Update()
    {
        if (!suspendControls)
        {
            currentState.UpdateBehavior();
        }

        

        if (hp.currentHealth == 0 && !noGhost)
        {
            ghost.GetComponent<AIController>().gotPlayer = true;
            heart.Stop();

            if (cam.child)
            {
                Destroy(cam.child.gameObject);
            }
        }

        AudioChecks();

        if (cursorDeactivated && Time.timeScale == 1f)
        {
            Cursor.visible = false;
            cursorDeactivated = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.I))
        {
            if (!cursorDeactivated)
            {
                //cursorDeactivated = true;
                Cursor.visible = true;
                StartCoroutine("CursorDelay");
            }
            
        }
        if (hp.currentHealth == 0f)
        {
            Cursor.visible = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (!suspendControls)
        {
            currentState.FixedUpdateBehavior();
            currentState.WalkRythm();
            HeartBeat();
        }

        
        
    }

    public void SetState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitBehavior();
        }
        currentState = newState;
        currentState.EntryBehavior();
    }

    public float GetXInput()
    {
        float input = Input.GetAxisRaw("Horizontal");
        return input;
    }

    public float GetZInput()
    {
        float input = Input.GetAxisRaw("Vertical");
        return input;
    }

    public void Move()
    {
        Vector3 moveDirection = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (running)
        {
            moveDirection = transform.forward;
        }

        RaycastHit hit;

        Debug.DrawRay(heart.transform.position, -transform.up * 1.25f, Color.red, 0.5f);
        if (Physics.SphereCast(heart.transform.position, 0.1f, -transform.up, out hit, 1.25f, groundMask))
        {
            //Debug.Log("The normal for " + hit.transform.gameObject + "is " + hit.normal);
            airborn = false;
            moveDirection = moveDirection - hit.normal * Vector3.Dot(moveDirection, hit.normal);
            rigbod.velocity = moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            airborn = true;
        }
        
        Debug.DrawRay(transform.position, moveDirection * 2f, Color.green, 0.5f);
    }

    public void DrainStamina(bool drain)
    {
        if (Time.timeScale == 1)
        {
            if (drain && stamina > 0)
            {
                stamina -= (runStaminaCost * Time.deltaTime);
            }
            else if (stamina < 100)
            {
                stamina += (staminaRegenRate * Time.deltaTime);
            }
        }
    }

    public void Flashlight(bool physical)
    {
        if (lights != null)
        {
            if (lights.Length == 0)
            {
                lights = cam.child.lights;
            }
            else if (lights[0] == null)
            {
                lights = cam.child.lights;
            }
            if (flashLightOn)
            {
                foreach (Light light in lights)
                {
                    light.enabled = false;
                    flashLightOn = false;
                    cam.child.FlipSwitch(physical);
                }
            }
            else
            {
                foreach (Light light in lights)
                {
                    light.enabled = true;
                    flashLightOn = true;
                    cam.child.FlipSwitch(physical);
                }
            }
        }
    }

    IEnumerator  Flicker()
    {
        float interval = Random.Range(0.06f, 0.08f);
        float interval2 = Random.Range(0.2f, 0.2f);

        int flickers = 24;
        if (flickers %2 != 0)
        {
            flickers--;
        }

        int i = 0;

        while (flickers != 0)
        {
            Flashlight(false);
            flickers--;

            if (i < 2)
            {
                i++;
                yield return new WaitForSeconds(interval2);
            }
            else if (i == flickers - 7)
            {
                i++;
                yield return new WaitForSeconds(0.2f);
            }
            else if (i == flickers)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }
        }
        yield return null;
    }

    public void FlashlightFlicker()
    {
        if (cam.child.switchedOn)
        {
            cam.child.flickerSound.Play();
            StartCoroutine("Flicker");
        } 
    }

    IEnumerator RandomFlicker()
    {
        if (transform.position.y < -2f && flashLightOn)
        {
            FlashlightFlicker();
        }
        yield return new WaitForSeconds(Random.Range(20f, 60f));
        //yield return new WaitForSeconds(10f);

        StartCoroutine("RandomFlicker");
    }

    public void ChangeSize()
    {
        if (isCrouching)
        {
            myCollider.height = 1;
            myCollider.center = new Vector3(0,0.5f, 0);
        }
        else
        {
            myCollider.height = 2;
            myCollider.center = new Vector3(0, 1f, 0);
        }
    }

    public void CalculateAdrenaline()
    {
        if (noGhost)
        {
            if (FindGhost() == null)
            {
                return;
            }
            else
            {
                ghost = FindGhost().transform;
                ghostScript = ghost.GetComponent<AIController>();
                noGhost = false;
            }
        }

        //Gonna need a story check to make sure it doesn't trigger through floors and ceilings
        ghostDistance = Vector3.Distance(transform.position, ghost.position);

        if (isBeingChased)
        {
            adrenalineLevel = 4;
        }
        else if (ghostDistance <= lvlOneThreshold)
        {
            if (ghostDistance <= lvlTwoThreshold)
            {
                if (ghostDistance <= lvlThreeThreshold)
                {
                    adrenalineLevel = 3;
                    return;
                }
                adrenalineLevel = 2;
                return;
            }
            adrenalineLevel = 1;
        }
        else
        {
            adrenalineLevel = 0;
        }
    }

    public GameObject FindGhost()
    {
        GameObject theGhost = GameObject.FindGameObjectWithTag("Enemy");
        return theGhost;
    }

    public void HeartBeat()
    {
        switch (adrenalineLevel)
        {
            case 0:
                tension = false;
                musicState = 0;
                heart.volume = 0f;
                if (gameController.basement && !gameController.basementStarted)
                {
                    gameController.ChangeMusic(3, 0.2f, false);
                }
                
                break;
            case 1:
                if (!isBeingChased && !tension && !gameController.playerIsSafe)
                {
                    gameController.ChangeMusic(1, 0.2f, false);
                    tension = true;
                }
                musicState = 1;
                heart.volume = 0.1f;
                break;
            case 2:
                if (!isBeingChased && !tension && !gameController.playerIsSafe)
                {
                    gameController.ChangeMusic(1, 0.2f, false);
                    tension = true;
                }
                musicState = 1;
                heart.volume = 0.2f;
                break;
            case 3:
                if (!isBeingChased && !tension && !gameController.playerIsSafe)
                {
                    gameController.ChangeMusic(1, 0.2f, false);
                    tension = true;
                }
                musicState = 1;
                heart.volume = 0.3f;
                break;
            case 4:
                tension = false;
                musicState = 1;
                heart.volume = 0.4f;
                break;
        }

        float pitchChange = ghostDistance / lvlOneThreshold;
        heart.pitch = 1 + (1 - Mathf.Clamp(pitchChange, 0f, 1f));
    }

    IEnumerator HeartCycle()
    {
        yield return null;
    }

    public bool CheckForItem()
    {
        for (int i = 0; i < itemScript.inventory.Length; i++)
        {
            if (itemScript.inventory[i].GetComponent<ItemProperties>().itemName == "Gold Key")
            {
                return true;
            }
        }
        return false;

       
    }

    private void AudioChecks()
    {
        if (transform.position.y < -2f)
        {
            if (!gameController.basementStarted && !isBeingChased && adrenalineLevel == 0)
            {
                gameController.ChangeMusic(3, 0f, true);
                gameController.basementStarted = true;
                gameController.ambientStarted = false;
            }
        }
        else
        {
            if (!gameController.ambientStarted && !isBeingChased)
            {
                gameController.ChangeMusic(0, 0f, true);
                gameController.ambientStarted = true;
            }

            gameController.basementStarted = false;
        }

        if (stamina <= 0 && Time.timeScale != 0f)
        {
            if (!breathing)
            {
                breath.Play();
                breathing = true;
            }

        }
        if (breathing && stamina >= 100f && Time.timeScale != 0f)
        {
            breath2.Play();
            breath.Stop();
            breathing = false;
        }
        if (Time.timeScale == 1f && breathPause && breathing)
        {
            breath.UnPause();
            breathPause = false;
        }
        else if (Time.timeScale == 0f || suspendControls)
        {
            breath.Pause();
            breathPause = true;
        }

        if (suspendControls)
        {
            foreach (AudioSource sound in footsteps)
            {
                sound.Stop();
            }
        }
    }

    IEnumerator AudioFadeIn()
    {
        //Debug.Log(AudioListener.volume);
        if (AudioListener.volume < 1f)
        {
            AudioListener.volume += fadeInSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            StartCoroutine("AudioFadeIn");
        }
        else
        {
            yield return null;
        }
        
    }

    IEnumerator CursorDelay()
    {
        //yield return new WaitForSeconds(1f);
        yield return new WaitForSecondsRealtime(1f);
        cursorDeactivated = true;
    }
}
