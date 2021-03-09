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
    public Light[] lights;
    public bool flashLightOn = false;
    public bool isCrouching = false;

    [SerializeField]
    private float stamina = 100f;

    private Rigidbody rigbod;
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

    public float viewTimer;
    public bool viewStep;


    private void Awake()
    {
        camera = Instantiate(camPrefab, transform.position, transform.rotation).GetComponent<FirstPersonCamera>();
        ogMoveSpeed = moveSpeed;
        ogRegenRate = staminaRegenRate;
        ogstepInterval = stepInterval;
        camera = GetComponentInChildren<FirstPersonCamera>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonCamera>();
        lights = camera.GetComponentsInChildren<Light>();
        rigbod = GetComponent<Rigidbody>();
        SetState(new PlayerIdle(this));       
    }

    void Update()
    {
        currentState.UpdateBehavior();
    }

    private void FixedUpdate()
    {
        currentState.WalkRythm();
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
        Vector3 direction = transform.rotation * new Vector3(input, 0, 0).normalized;

        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * collisionDistance, Color.red, 0.5f);
        /*if (Physics.SphereCast(transform.position, collisionScale, direction, out hit, collisionDistance))
        {
            input = 0f;
        }
        Debug.Log(input);*/
        return input;
    }

    public float GetZInput()
    {
        float input = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.rotation * new Vector3(0, 0, input).normalized;

        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * collisionDistance, Color.red, 0.5f);
        /*
        if (Physics.SphereCast(transform.position, collisionScale, direction, out hit, collisionDistance))
        {
            input = 0f;
        }
        Debug.Log(input);*/
        return input;
    }

    public void Move()
    {
        Vector3 moveDirection = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            moveDirection = moveDirection - hit.normal * Vector3.Dot(moveDirection, hit.normal);
        }
        rigbod.velocity = moveDirection * moveSpeed * Time.deltaTime;
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
        if (flashLightOn)
        {
            foreach (Light light in lights)
            {
                light.enabled = false;
                flashLightOn = false;
            }
        }
        else
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
                flashLightOn = true;
            }
        }
    }

    public void Crouch()
    {
        //camera.Move();
    }
}
