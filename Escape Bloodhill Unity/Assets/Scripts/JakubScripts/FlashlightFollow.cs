﻿using System.Collections;
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
    public bool deactivateOnTurn = false;

    private Vector3 onPosition, offPosition;
    public bool switchedOn = false;
    public AudioSource switchClickOn, switchClickOff, flickerSound;

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
    public float runMoveMagnitude;
    public float crouchBobMagnitude;

    public float moveTilt = 0f;
    public float walkTilt;
    public float runTilt;
    public float crouchTilt;

    public Texture2D flashLight, keyLight;

    void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        model = GetComponent<MeshRenderer>();
        switchModel = transform.GetChild(0).GetComponent<MeshRenderer>();
        offPosition = switchModel.transform.localPosition;
        onPosition = transform.GetChild(2).localPosition;
        lightSource = transform.GetChild(1);
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
            lights[0].enabled = true;
            LightRay();
        }
        else if (lights[0].enabled)
        {
            lights[0].enabled = false;
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
    }

    void LateUpdate()
    {
        if (parent != null)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90 + moveTilt, 0, 0), 4f * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 4f * Time.deltaTime);
        }

    }

    public void SetParent(Transform newParent)
    {
        parent = newParent;
        parentScript = parent.GetComponent<FirstPersonCamera>();
    }

    public void FlipSwitch(bool physical)
    {
        if (switchedOn)
        {
            if (physical)
            {
                switchClickOff.Play();
                switchModel.transform.localPosition = offPosition;
            }

            switchedOn = false;
        }
        else
        {
            if (physical)
            {
                switchClickOn.Play();
                switchModel.transform.localPosition = onPosition;
            }
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
