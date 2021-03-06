using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private Vector2 mouseVector, smoothVector;

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

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ogStandHeight = standHeight;
        ogCrouchHeight = crouchHeight;
        ogMagnitude = walkBobMagnitude;
        ogSwayFactor = swayFactor;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 targetPosition;

        transform.rotation = parent.transform.rotation * Quaternion.AngleAxis(-mouseVector.y, parent.transform.right);
        transform.rotation = parent.transform.rotation * Quaternion.Euler(-mouseVector.y, 0, 1);

        if (parent.isCrouching)
        {
            targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + crouchHeight, parent.transform.position.z + swayFactor);
        }
        else
        {
            targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + standHeight, parent.transform.position.z + swayFactor);
        }       
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }

    public void Look()
    {
        var mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseChange = Vector2.Scale(mouseChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseChange.x, 1f / smoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseChange.y, 1f / smoothing);
        mouseVector += smoothVector;
        mouseVector = new Vector2(mouseVector.x, Mathf.Clamp(mouseVector.y, -44, 60));

        parent.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, Vector3.up);
    }

    public void Move(float targetHeight)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.075f);
    }
}