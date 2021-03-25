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
    public GameObject wallWithPassword;
    public KeyCode startTyping;
    public string thisIsPassword;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        passwordPlease.SetActive(false);
        instructions.SetActive(false);
        //Debug.Log(Random.Range(0, 40) + " " + Random.Range(0, 40) + " " + Random.Range(0, 40));
        thisIsPassword = Random.Range(0, 40) + " " + Random.Range(0, 40) + " " + Random.Range(0, 40);
        Debug.Log(thisIsPassword);
        wallWithPassword.GetComponent<Text>().text = "The safe combination is \n" + thisIsPassword;        
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("MainCamera");

        if (thisIsPassword == passwordPlease.GetComponent<TypePasscode>().password)
        {
            CorrectPassword();
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
            passwordPlease.GetComponent<TypePasscode>().password = "";
            passwordPlease.GetComponent<TypePasscode>().fromKeyboard.text = "";
            passwordPlease.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(startTyping))
            {
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
    public void CorrectPassword()
    {
        instructions.SetActive(false);
        passwordPlease.GetComponent<TypePasscode>().password = "";
        passwordPlease.GetComponent<TypePasscode>().fromKeyboard.text = "";
        passwordPlease.SetActive(false);
        gameObject.GetComponent<OpenDoor>().openTheDoor = true;
        player.GetComponent<FirstPersonCamera>().enabled = true;
        Destroy(gameObject.GetComponent<AskForPassword>(), 0.0f);
    }
}