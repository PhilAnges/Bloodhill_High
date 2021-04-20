using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class AskForPasswordTwo : MonoBehaviour 
{
    public string playerTag;
    public GameObject player;
    public GameObject passwordPlease;
    public GameObject instructions;
    public GameObject textBackground;
    public KeyCode startTyping;
    public string thisIsPassword;

    // Start is called before the first frame update 
    void Start() 
    {
        textBackground.SetActive(false);
        player = GameObject.FindGameObjectWithTag("MainCamera");
        passwordPlease.SetActive(false);
        instructions.SetActive(false);
        gameObject.GetComponent<StopTime>().enabled = false;
    } 
     
    // Update is called once per frame 
    void Update() 
    { 
        //player = GameObject.FindGameObjectWithTag("MainCamera");
        if (thisIsPassword.ToLower() == passwordPlease.GetComponent<TypePasscode>().password.ToLower())
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
            textBackground.SetActive(false);
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
                //player.GetComponent<FirstPersonCamera>().enabled = !player.GetComponent<FirstPersonCamera>().enabled;
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
        gameObject.GetComponent<OpenDoor>().openTheDoor = true;
        //player.GetComponent<FirstPersonCamera>().enabled = true;
        Destroy(gameObject.GetComponent<AskForPasswordTwo>(), 0.0f);
    }
}