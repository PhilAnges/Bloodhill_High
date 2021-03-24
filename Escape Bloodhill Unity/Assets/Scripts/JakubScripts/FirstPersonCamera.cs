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

    public bool lookingBack = false;
    public float lookSpeed;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        targetPosition = new Vector3(parent.transform.position.x + 0.34f, parent.transform.position.y - 0.31f, parent.transform.position.z + 0.57f);
        Destroy(GameObject.Find("Flashlight"));
        child = Instantiate(flashlightPrefab, targetPosition, Quaternion.Euler(90f, 0f ,0f)).GetComponent<FlashlightFollow>();
        child.SetParent(this.transform);
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
        if (parent && parent.hp.currentHealth != 0)
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

            if (Input.GetAxis("LookBack") != 0)
            {
                targetRotation = parent.transform.rotation * Quaternion.AngleAxis(180f, parent.transform.up) * Quaternion.Euler(-mouseVector.y, 0, 0);
            }
            else
            {
                targetRotation = parent.transform.rotation * Quaternion.Euler(-mouseVector.y, 0, 0);
            }

            if (parent.isCrouching)
            {
                targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + crouchHeight, parent.transform.position.z) + (parent.transform.right * swayFactor) - (parent.transform.forward * farBackness);
            }
            else
            {
                targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y + standHeight, parent.transform.position.z) + (parent.transform.right * swayFactor) - (parent.transform.forward * farBackness);
            }
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition , 0.1f);
    }

    public void Look()
    {
        if (parent)
        {
            parent.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, Vector3.up);
        }
    }
}