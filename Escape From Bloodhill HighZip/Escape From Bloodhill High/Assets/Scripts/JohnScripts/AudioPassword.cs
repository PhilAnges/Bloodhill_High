using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPassword : MonoBehaviour
{
    public string playerTag;
    public AudioSource audioPassword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if(other.gameObject.GetComponent<ItemPickup>().pickedUpItem == true)
            {
                audioPassword.Play();
                
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioPassword.Stop();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (other.gameObject.GetComponent<ItemPickup>().pickedUpItem == true)
            {
                if (audioPassword.isPlaying == false)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}