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
    public GameObject textBackground;
    public GameObject positionOne;
    public GameObject positionTwo;
    public GameObject positionThree;
    public KeyCode startTyping;
    public string thisIsPassword;

    private int numberOne;
    private int numberTwo;
    private int numberThree;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        passwordPlease.SetActive(false);
        instructions.SetActive(false);
        textBackground.SetActive(false);

        numberOne = Random.Range(0, 40);
        numberTwo = Random.Range(0, 40);
        numberThree = Random.Range(0, 40);
        gameObject.GetComponent<StopTime>().enabled = false;

        thisIsPassword = numberOne + " " + numberTwo + " " + numberThree;
        Debug.Log(thisIsPassword);
        positionOne.GetComponent<Text>().text = numberOne + "";
        positionTwo.GetComponent<Text>().text = numberTwo + "";
        positionThree.GetComponent<Text>().text = numberThree + "";


    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (thisIsPassword == passwordPlease.GetComponent<TypePasscode>().password)
        {
            CorrectPassword();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameObject.GetComponent<StopTime>().enabled = true;
            instructions.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameObject.GetComponent<StopTime>().enabled = false;
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
            }
        }
    }

    public void DisableAskForPassword(bool isActive)
    {
        if (isActive == false)
        {
            passwordPlease.SetActive(true);
            textBackground.SetActive(true);
        }
        if (isActive == true)
        {
            passwordPlease.SetActive(false);
            textBackground.SetActive(false);
        }
    }
    public void CorrectPassword()
    {
        textBackground.SetActive(false);
        gameObject.GetComponent<StopTime>().enabled = false;
        Time.timeScale = 1f;
        instructions.SetActive(false);
        passwordPlease.GetComponent<TypePasscode>().password = "";
        passwordPlease.GetComponent<TypePasscode>().fromKeyboard.text = "";
        passwordPlease.SetActive(false);
        gameObject.GetComponent<SafeDoor>().openTheDoor = true;
        Destroy(gameObject.GetComponent<AskForPassword>(), 0.0f);
    }        
}