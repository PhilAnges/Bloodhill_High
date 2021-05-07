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

    public Transform flashlightTransform;

    private bool inputReset = false;

    private Camera mainCam;
    public Camera flashCam;
    public GameObject secondCam;
    public Transform noKeyHole;
    public float keyDistance;
    public bool testBool = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(GameObject.Find("Flashlight"));
        child = Instantiate(flashlightPrefab, flashlightTransform.position,transform.rotation * 
            Quaternion.Euler(90f, 0f ,0f), this.transform).GetComponent<FlashlightFollow>();
        child.SetParent(this.transform);
        ogStandHeight = standHeight;
        ogCrouchHeight = crouchHeight;
        ogMagnitude = walkBobMagnitude;
        ogSwayFactor = swayFactor;
        mouseVector = new Vector2(-90f, 0f);
        mainCam = GetComponent<Camera>();
        noKeyHole = GameObject.Find("Jakub Programming").transform.Find("NoKeyHole");
    }

    void Update()
    {
        keyDistance =  Vector3.Distance(transform.position, noKeyHole.position);

        if (mainCam.enabled == false && keyDistance > 4)
        {
            foreach (Light light in child.lights)
            {
                light.cookie = child.keyLight;
            }
        }
        else
        {

            foreach (Light light in child.lights)
            {
                light.cookie = child.flashLight;
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        Vector2 lookChange = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        if (Cursor.lockState != CursorLockMode.Locked && inputReset)
        {
            Input.ResetInputAxes();
            lookChange = Vector2.zero;
            inputReset = false;
        }

        if (parent && parent.hp.currentHealth != 0 && !parent.suspendControls)
        {
            if (parent && Cursor.lockState == CursorLockMode.Locked)
            {
                mouseVector += lookChange;
                mouseVector = new Vector2(mouseVector.x, Mathf.Clamp(mouseVector.y, -44, 60));

                parent.transform.rotation = Quaternion.AngleAxis(mouseVector.x, Vector3.up);
            }

            if (Input.GetAxis("LookBack") != 0 && !parent.airborn)
            {
                targetRotation = parent.transform.rotation * 
                    Quaternion.AngleAxis(180f, parent.transform.up) * 
                    Quaternion.Euler(-mouseVector.y, 0, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(-mouseVector.y, mouseVector.x, 0);
            }

            if (parent.isCrouching)
            {
                targetPosition = new Vector3(parent.transform.position.x,
                    parent.transform.position.y + crouchHeight,
                    parent.transform.position.z) + 
                    (parent.transform.right * swayFactor) - 
                    (parent.transform.forward * farBackness);
            }
            else
            {
                targetPosition = new Vector3(parent.transform.position.x,
                    parent.transform.position.y + standHeight,
                    parent.transform.position.z) + 
                    (parent.transform.right * swayFactor) - 
                    (parent.transform.forward * farBackness);
            }
        }
        if (!parent.suspendControls)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, lookSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
        }
        
    }

    public void Look()
    {
        if (parent)
        {
            if (!parent.suspendControls)
            {

                parent.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, Vector3.up);
            }
            
        }
    }

    public void SetParent(PlayerController player)
    {
        parent = player;
        targetPosition = new Vector3(parent.transform.position.x + 0.34f,
            parent.transform.position.y - 0.31f, parent.transform.position.z + 0.57f);
    }

    public void ChangeCam()
    {
        mainCam.enabled = false;
        flashCam.enabled = false;
        secondCam.SetActive(true);
    }
}