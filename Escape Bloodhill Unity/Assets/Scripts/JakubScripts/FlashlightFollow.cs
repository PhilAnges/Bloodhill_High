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
    private bool parentSet = false;


    // Start is called before the first frame update
    void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        model = GetComponent<MeshRenderer>();
        switchModel = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
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

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);

        //targetPosition = Quaternion.Euler(0f, parent.transform.rotation.y, 0f) * targetPosition;


        transform.rotation = parent.transform.rotation * Quaternion.Euler(90, 0, 1);
        transform.position = Vector3.Lerp(transform.position, targetPosition + (parent.transform.right * 0.25f) - (parent.transform.forward * -0.4f) - (parent.transform.up * 0.3f), 0.4f);
        //transform.rotation = parent.transform.rotation * Quaternion.Euler(90, 0, 1);
    }

    public void SetParent(Transform newParent)
    {
        parent = newParent;
        parentScript = parent.GetComponent<FirstPersonCamera>();
    }
}
