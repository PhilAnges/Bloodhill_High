using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;
    public float collisionDistance = 2f;
    public float collisionScale = 0.5f;

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
    public FirstPersonCamera camera;
    public Transform flashlight;
    public Light[] lights;
    public bool flashLightOn = false;
    public bool isCrouching = false;

    [SerializeField]
    private float stamina = 100f;

    public Rigidbody rigbod;
    public  GameObject camPrefab;
    
    public float stepInterval = 2f;
    public float ogstepInterval;
    public float runInterval = 0.2f;
    public float crouchInterval = 0.5f;

    public float runBobIntensity = 0.2f;
    public float crouchBobIntensity = 0.05f;
    public float walkSwayIntensity = 0.2f;
    public float runSwayIntensity = 0.2f;
    public float crouchSwayIntensity = 0.1f;

    public float flickTimer;

    private CapsuleCollider collider;

    public int adrenalineLevel = 0;

    public float lvlOneThreshold;
    public float lvlTwoThreshold;
    public float lvlThreeThreshold;

    private Transform ghost;

    public int noiseLevel = 0;
    public bool isHidden = false;
    public bool noGhost = false;

    private AudioSource heart;
    public bool isBeingChased = false;
    private float ghostDistance;
    public bool running = false;
    public PlayerHealth hp;
    public bool airborn = false;




    private void Awake()
    {
        camera = Instantiate(camPrefab, transform.position, transform.rotation).GetComponent<FirstPersonCamera>();
        camera.SetParent(this);
        ogMoveSpeed = 100f;
        ogRegenRate = staminaRegenRate;
        ogstepInterval = stepInterval;
        rigbod = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        heart = GetComponentInChildren<AudioSource>();
        hp = GetComponent<PlayerHealth>();

        if (FindGhost() == null)
        {
            noGhost = true;
        }
        else
        {
            ghost = FindGhost().transform;
        }

        GetComponent<PlayerHealth>().gameOverMenu = GameObject.Find("John's Programming Box").transform.Find("GameOver").gameObject;
        GameObject.Find("John's Programming Box").transform.Find("Safe Room").GetComponent<SafeRoom>().player = this.gameObject;

        SetState(new PlayerIdle(this));       
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
        camera.Look();
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

        Debug.DrawRay(heart.transform.position, -transform.up * 2f, Color.red, 0.5f);
        if (Physics.Raycast(heart.transform.position, -transform.up, out hit, 2f))
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

    public void Flashlight()
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
                    camera.child.FlipSwitch();
                }
            }
            else
            {
                foreach (Light light in lights)
                {
                    light.enabled = true;
                    flashLightOn = true;
                    camera.child.FlipSwitch();
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
            Flashlight();
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
        Debug.Log("Running CalculateAdrenaline");

        if (noGhost)
        {
            if (FindGhost() == null)
            {
                return;
            }
            else
            {
                ghost = FindGhost().transform;
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
        //Temp
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
                //heart.pitch = 1f;
                heart.volume = 0f;
                break;
            case 1:
                //heart.pitch = 1f;
                heart.volume = 0.2f;
                break;
            case 2:
                //heart.pitch = 1.4f;
                heart.volume = 0.3f;
                break;
            case 3:
                //heart.pitch = 1.6f;
                heart.volume = 0.4f;
                break;
            case 4:
                //heart.pitch = 1.6f;
                heart.volume = 1f;
                break;
        }
        float pitchChange = ghostDistance / lvlOneThreshold;
        heart.pitch = 1 + (1 - Mathf.Clamp(pitchChange, 0f, 1f));
    }

    IEnumerator HeartCycle()
    {
        yield return null;
    }
}
