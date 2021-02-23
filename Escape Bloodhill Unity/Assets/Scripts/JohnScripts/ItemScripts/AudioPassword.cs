using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPassword : MonoBehaviour
{
    public string playerTag;
    public string itemTag;
    public AudioSource audioPassword;

    private bool hasItem;
    
    private GameObject hold;

    // Start is called before the first frame update
    void Start()
    {
        hasItem = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            for(int i=0; i<other.gameObject.GetComponent<ItemPickup>().inventory.Length; i++)
            {
                hold = other.gameObject.GetComponent<ItemPickup>().inventory[i];
                if (hold.CompareTag(itemTag))
                {
                    hasItem = true;
                    break;
                }
            }
            if(hasItem == true)
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
            if (hasItem == true)
            {
                if (audioPassword.isPlaying == false)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}