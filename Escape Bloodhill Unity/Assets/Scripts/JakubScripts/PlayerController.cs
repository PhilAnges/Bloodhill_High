using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;
    public float collisionDistance = 2f;
    public float collisionScale = 0.5f;


    public float moveSpeed = 2f;
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
    public Light flashlight;
    public Light flashlight2;
    public bool flashLightOn = false;
    public bool isCrouching = false;

    [SerializeField]
    private float stamina = 100f;

    private Rigidbody rigbod;


    private void Awake()
    {
        ogMoveSpeed = moveSpeed;
        ogRegenRate = staminaRegenRate;
        camera = GetComponentInChildren<FirstPersonCamera>();
        flashlight = camera.transform.GetChild(0).GetComponent<Light>();
        flashlight2 = camera.transform.GetChild(1).GetComponent<Light>();
        rigbod = GetComponent<Rigidbody>();
        SetState(new PlayerIdle(this));       
    }

    void Update()
    {
        currentState.UpdateBehavior();
    }

    private void FixedUpdate()
    {
        
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
        if (Physics.SphereCast(transform.position, collisionScale, direction, out hit, collisionDistance))
        {
            input = 0f;
        }
        Debug.Log(input);
        return input;
    }

    public float GetZInput()
    {
        float input = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.rotation * new Vector3(0, 0, input).normalized;

        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * collisionDistance, Color.red, 0.5f);
        if (Physics.SphereCast(transform.position, collisionScale, direction, out hit, collisionDistance))
        {
            input = 0f;
        }
        Debug.Log(input);
        return input;
    }

    public void Move()
    {
        transform.Translate(new Vector3(GetXInput(), 0, GetZInput()).normalized * moveSpeed * Time.deltaTime);
        //Vector3 newPos = transform.rotation * new Vector3(GetXInput(), 0, GetZInput()).normalized;
        //rigbod.velocity = newPos * 1000 * Time.deltaTime;
        //Debug.DrawRay(transform.position, newPos * collisionDistance, Color.green, 0.5f);
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
            flashlight.enabled = false;
            flashlight2.enabled = false;
            flashLightOn = false;
        }
        else
        {
            flashlight.enabled = true;
            flashlight2.enabled = true;
            flashLightOn = true;
        }
    }

    public void Crouch()
    {
        //camera.Move();
    }
}
