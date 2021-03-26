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


    // Start is called before the first frame update
    void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        model = GetComponent<MeshRenderer>();
        switchModel = transform.GetChild(0).GetComponent<MeshRenderer>();
        offPosition = switchModel.transform.localPosition;
        onPosition = transform.GetChild(4).localPosition;
        switchClick = GetComponent<AudioSource>();
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
    }

    void LateUpdate()
    {
        if (parent != null)
        {
            Vector3 targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z) + (parent.transform.right * 0.25f) - (parent.transform.forward * -0.4f) - (parent.transform.up * 0.3f);

            transform.rotation = Quaternion.Slerp(transform.rotation, parent.transform.rotation * Quaternion.Euler(90, 0, 0), 20f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.4f);
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
}
