using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Vector2 mouseVector, smoothVector;

    [Range(0.1f, 10.0f)]
    public float sensitivity = 5.0f;
    [Range(0.1f, 5.0f)]
    public float smoothing = 2.0f;
    [Range(0.5f, 2f)]
    public float height = 0.1f;
    [Range(0.5f, 2f)]
    public float crouchHeight = 1f;
    public float ogCrouchHeight;
    //[Range(0.5f, 2f)]
    public float standHeight = 1.5f;
    public float ogStandHeight;


    public float walkBobMagnitude;
    public float ogMagnitude;

    public float swayFactor = 0f;
    public float ogSwayFactor;

    public PlayerController parent;
    public FlashlightFollow child;
    public GameObject flashlightPrefab;

    [Range(-0.3f, 0.5f)]
    public float farBackness = 1f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Vector3 targetPosition = new Vector3(parent.transform.position.x + 0.34f, parent.transform.position.y - 0.31f, parent.transform.position.z + 0.57f);

        child = Instantiate(flashlightPrefab, targetPosition, Quaternion.Euler(90f, 0f ,0f)).GetComponent<FlashlightFollow>();
        child.parent = this.transform;
        parent.lights = child.GetComponentsInChildren<Light>();
        ogStandHeight = standHeight;
        ogCrouchHeight = crouchHeight;
        ogMagnitude = walkBobMagnitude;
        ogSwayFactor = swayFactor;
    }

    void Update()
    {
        parent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        if (parent)
        {
            Vector3 targetPosition;

            transform.rotation = parent.transform.rotation * Quaternion.AngleAxis(-mouseVector.y, parent.transform.right);
            transform.rotation = parent.transform.rotation * Quaternion.Euler(-mouseVector.y, 0, 1);

            if (parent.isCrouching)
            {
                targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + crouchHeight, parent.transform.position.z);
            }
            else
            {
                targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + standHeight, parent.transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition + (parent.transform.right * swayFactor) - (parent.transform.forward * farBackness), 0.1f);
        }

        
    }

    public void Look()
    {
        if (parent)
        {
            Vector2 lookChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            lookChange = Vector2.Scale(lookChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothVector.x = Mathf.Lerp(smoothVector.x, lookChange.x, 1f / smoothing);
            smoothVector.y = Mathf.Lerp(smoothVector.y, lookChange.y, 1f / smoothing);
            mouseVector += smoothVector;
            mouseVector = new Vector2(mouseVector.x, Mathf.Clamp(mouseVector.y, -44, 60));

            parent.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, Vector3.up);
        }
    }

    public void Move(float targetHeight)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.075f);
    }
}