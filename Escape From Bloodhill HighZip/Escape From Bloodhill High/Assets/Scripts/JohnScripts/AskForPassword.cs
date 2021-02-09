using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskForPassword : MonoBehaviour
{
    public string playerTag;
    public GameObject passwordPlease;
    public string thisIsPassword;

    // Start is called before the first frame update
    void Start()
    {
        passwordPlease.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(thisIsPassword == passwordPlease.GetComponent<TypePasscode>().password)
        {
            passwordPlease.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            passwordPlease.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            passwordPlease.SetActive(false);
        }
    }
}
