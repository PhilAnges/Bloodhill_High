using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform parent;
    public float swayFactor = 0;
    public float runSwayIntensity = 2f;
    public float farBackness = 0;
    public bool active = false;
    public Light[] lights;
    private MeshRenderer model;
    private MeshRenderer switchModel;
    private FirstPersonCamera parentScript;
    private bool parentSet = false;
    public bool deactivateOnTurn = false;

    private Vector3 onPosition, offPosition;
    private bool switchedOn = false;
    public AudioSource switchClick;

    public Transform lightSource;
    private Vector3 velocity;

    public Vector3 centerPosition;
    private Vector3 centerRotation;
    private Vector3 targetPosition;

    public Vector3 horizontalBounds, verticalBounds;
    public float bobHeight = 0f;
    public float moveDepth = 0f;
    public float bobMagnitude;
    public float swayMagnitude;
    public float moveMagnitude;
    public float runBobMagnitude;

    public float moveTilt = 0f;
    public float walkTilt;



    // Start is called before the first frame update
    void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        model = GetComponent<MeshRenderer>();
        switchModel = transform.GetChild(0).GetComponent<MeshRenderer>();
        offPosition = switchModel.transform.localPosition;
        onPosition = transform.GetChild(4).localPosition;
        switchClick = GetComponent<AudioSource>();
        lightSource = transform.GetChild(3);
        //centerPosition = transform.localPosition;
        centerRotation = transform.localEulerAngles;
        centerPosition = new Vector3(0.261f, -0.285f, 0.398f);
    }

    private void Update()
    {
        if (parentScript.parent.hp.currentHealth == 0)
        {
            Destroy(this.gameObject);
        }

        if (parent != null)
        {
            if (Input.GetAxis("LookBack") != 0 && deactivateOnTurn)
            {
                model.enabled = false;
                switchModel.enabled = false;
                foreach (Light light in lights)
                {
                    light.enabled = false;
                }
            }
            else
            {
                model.enabled = true;
                switchModel.enabled = true;

                if (parentScript.parent.flashLightOn)
                {
                    foreach (Light light in lights)
                    {
                        light.enabled = true;
                    }
                }
            }
        }

        if (switchedOn)
        {
            LightRay();
        }

        targetPosition = centerPosition;

        if (Input.GetAxis("Mouse X") > 0.3f)
        {
            targetPosition = centerPosition + horizontalBounds;
        }
        else if (Input.GetAxis("Mouse X") < -0.3f)
        {
            targetPosition = centerPosition - horizontalBounds;
        }

        if (Input.GetAxis("Mouse Y") > 0.3f)
        {
            targetPosition += verticalBounds;
        }
        else if (Input.GetAxis("Mouse Y") < -0.3f)
        {
            targetPosition -= verticalBounds;
        }

        targetPosition += (transform.forward * bobHeight);
        targetPosition += (Vector3.forward * moveDepth);


        //Debug.Log(targetPosition);
    }

    void LateUpdate()
    {
        if (parent != null)
        {
            //targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z) + (parent.transform.right * 0.25f) - (parent.transform.forward * -0.4f) - (parent.transform.up * 0.3f);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, 40f * Time.deltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.05f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90 + moveTilt, 0, 0), 5f * Time.deltaTime);
            //transform.localPosition = targetPosition;
            //transform.rotation = parent.transform.rotation * Quaternion.Euler(90, 0, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 5f * Time.deltaTime);
        }   
    }

    public void SetParent(Transform newParent)
    {
        parent = newParent;
        parentScript = parent.GetComponent<FirstPersonCamera>();
    }

    public void FlipSwitch()
    {
        if (switchedOn)
        {
            switchClick.Play();
            switchModel.transform.localPosition = offPosition;
            switchedOn = false;
        }
        else
        {
            switchClick.Play();
            switchModel.transform.localPosition = onPosition;
            switchedOn = true;
        }
    }

    public void LightRay()
    {
        RaycastHit hit;

        Debug.DrawRay(lightSource.position, lightSource.forward * 20f, Color.yellow, 0.5f);
        
        if (Physics.SphereCast(lightSource.position, 0.1f, transform.up, out hit, 20f))
        {
            if (!parentScript.parent.noGhost)
            {
                //Debug.Log(hit.collider.gameObject);
                if (hit.collider.tag == "Enemy")
                {
                    parentScript.parent.ghostScript.lit = true;
                }
                else
                {
                    parentScript.parent.ghostScript.lit = false;
                }
            }
            
        }
        
    }
}
