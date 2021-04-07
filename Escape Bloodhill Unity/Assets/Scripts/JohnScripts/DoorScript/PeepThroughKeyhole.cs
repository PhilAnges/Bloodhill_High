using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepThroughKeyhole : MonoBehaviour
{
    public GameObject playerCamera;
    public Camera doorCamera;
    public KeyCode actionKey;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        doorCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");


    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(actionKey))
            {
            playerCamera.GetComponentInChildren<Camera>().enabled = false;
            doorCamera.enabled = true;
            }
            if (Input.GetKeyUp(actionKey))
            {
            doorCamera.enabled = false;
            playerCamera.GetComponentInChildren<Camera>().enabled = true;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorCamera.enabled = false;
            playerCamera.GetComponentInChildren<Camera>().enabled = true;
        }
    }
}