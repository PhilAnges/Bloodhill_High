using System.Collections;
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
    public FirstPersonCamera camera;
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

    [SerializeField]
    private float stamina = 100f;
    [HideInInspector]
    public Transform ghost;
    private float ghostDistance;
    private CapsuleCollider collider;
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
    [HideInInspector]
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

    

    private void Awake()
    {
        Instantiate(screenFadePrefab);

        camera = Instantiate(camPrefab, transform.position, transform.rotation).GetComponent<FirstPersonCamera>();
        camera.SetParent(this);
        ogMoveSpeed = moveSpeed;
        ogRegenRate = staminaRegenRate;
        ogstepInterval = stepInterval;
        rigbod = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        heart = GetComponentInChildren<AudioSource>();
        hp = GetComponent<PlayerHealth>();
        itemScript = GetComponent<ItemPickup>();

        if (FindGhost() == null)
        {
            noGhost = true;
        }
        else
        {
            ghost = FindGhost().transform;
            ghostScript = ghost.GetComponent<AIController>();
        }

        GetComponent<PlayerHealth>().gameOverMenu = GameObject.Find("John's Programming Box").transform.Find("GameOver").gameObject;
        GameObject.Find("John's Programming Box").transform.Find("Safe Room").GetComponent<SafeRoom>().player = this.gameObject;

        SetState(new PlayerIdle(this));
        StartCoroutine("RandomFlicker");
    }

    void Update()
    {
        currentState.UpdateBehavior();

        if (hp.currentHealth == 0 && !noGhost)
        {
            ghost.GetComponent<AIController>().gotPlayer = true;

            if (camera.child)
            {
                Destroy(camera.child.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateBehavior();
        currentState.WalkRythm();
        HeartBeat();
    }

    private void LateUpdate()
    {
        //camera.Look();
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
        if (Physics.SphereCast(heart.transform.position, 0.2f, -transform.up, out hit, 1.25f))
        {
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
        if (drain && stamina > 0)
        {
            stamina -= (runStaminaCost * Time.deltaTime);
        }
        else if (stamina < 100)
        {
            stamina += (staminaRegenRate * Time.deltaTime);
        }
    }

    public void Flashlight(bool physical)
    {
        if (lights != null)
        {
            if (lights.Length == 0)
            {
                lights = camera.child.lights;
            }
            else if (lights[0] == null)
            {
                lights = camera.child.lights;
            }
            if (flashLightOn)
            {
                foreach (Light light in lights)
                {
                    light.enabled = false;
                    flashLightOn = false;
                    camera.child.FlipSwitch(physical);
                }
            }
            else
            {
                foreach (Light light in lights)
                {
                    light.enabled = true;
                    flashLightOn = true;
                    camera.child.FlipSwitch(physical);
                }
            }
        }
    }

    IEnumerator  Flicker()
    {
        float interval = Random.Range(0.06f, 0.08f);
        float interval2 = Random.Range(0.08f, 0.2f);
        int flickers = Random.Range(6, 12);
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
            else
            {
                yield return new WaitForSeconds(interval);
            }
        }
        yield return null;
    }

    public void FlashlightFlicker()
    {
        if (flashLightOn)
        {
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
        camera.child.flickerSound.Play();
        StartCoroutine("RandomFlicker");
    }

    public void ChangeSize()
    {
        if (isCrouching)
        {
            collider.height = 1;
            collider.center = new Vector3(0,0.5f, 0);
        }
        else
        {
            collider.height = 2;
            collider.center = new Vector3(0, 1f, 0);
        }
    }

    public void CalculateAdrenaline()
    {
        //Debug.Log("Running CalculateAdrenaline");

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
            if (!tension)
            {
                //ghostScript.gameController.ChangeMusic(1, 0f);
                tension = true;
            }
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
        //Temp
        else
        {
            if (tension)
            {
                //ghostScript.gameController.ChangeMusic(0, 0f);
                tension = false;
            }
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
                //heart.pitch = 1f;
                
                musicState = 0;
                heart.volume = 0f;
                break;
            case 1:
                //heart.pitch = 1f;
                
                musicState = 1;
                heart.volume = 0.2f;
                break;
            case 2:
                //heart.pitch = 1.4f;
                musicState = 1;
                heart.volume = 0.3f;
                break;
            case 3:
                //heart.pitch = 1.6f;
                musicState = 1;
                heart.volume = 0.4f;
                break;
            case 4:
                //heart.pitch = 1.6f;
                musicState = 1;
                heart.volume = 1f;
                break;
        }

        float pitchChange = ghostDistance / lvlOneThreshold;
        /*
        if (musicState == 1 && !isBeingChased && !tension)
        {
            ghostScript.gameController.ChangeMusic(1);
            Debug.Log("Starting Tension Music");
            tension = true;
            safety = false;
        }
        else if (musicState == 0 && !isBeingChased && !safety)
        {
            tension = false;
            ghostScript.gameController.ChangeMusic(0);
            safety = true;
        }
        if (musicState == 1)
        {
            ghostScript.gameController.music[1].volume = 1 + (1 - Mathf.Clamp(pitchChange, 0f, 1f));
        }
        */
        heart.pitch = 1 + (1 - Mathf.Clamp(pitchChange, 0f, 1f));
        if (tension && !isBeingChased)
        {
            ghostScript.gameController.music[1].volume = (1 - Mathf.Clamp(pitchChange, 0f, 1f));
        }
        //ghostScript.gameController.music[1].volume = 1 + (1 - Mathf.Clamp(pitchChange, 0f, 1f));

    }

    IEnumerator HeartCycle()
    {
        yield return null;
    }

    public bool CheckForItem()
    {
        if (itemScript.inventory[4] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
