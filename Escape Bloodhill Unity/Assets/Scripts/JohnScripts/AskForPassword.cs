using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskForPassword : MonoBehaviour
{
    public string playerTag;
    public GameObject player;
    public GameObject passwordPlease;
    public GameObject instructions;
    public KeyCode startTyping;
    public string thisIsPassword;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        passwordPlease.SetActive(false);
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");

        if (thisIsPassword == passwordPlease.GetComponent<TypePasscode>().password)
        {
            player.GetComponent<FirstPersonCamera>().enabled = true;
            instructions.SetActive(false);
            passwordPlease.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            instructions.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            instructions.SetActive(false);
            passwordPlease.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(startTyping))
            {
                //passwordPlease.SetActive(true);
                DisableAskForPassword(passwordPlease.activeSelf);
                player.GetComponent<FirstPersonCamera>().enabled = !player.GetComponent<FirstPersonCamera>().enabled;
            }
        }
    }

    public void DisableAskForPassword(bool isActive)
    {
        if (isActive == false)
        {
            passwordPlease.SetActive(true);
        }
        if (isActive == true)
        {
            passwordPlease.SetActive(false);
        }
    }
}