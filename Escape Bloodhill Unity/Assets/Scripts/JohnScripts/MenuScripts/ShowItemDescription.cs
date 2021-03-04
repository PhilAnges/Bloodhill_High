using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemDescription : MonoBehaviour
{
    public GameObject itemHeld;
    public GameObject descriptionBox;
    public GameObject playerCharacter;
    public bool isAudioDevice;


    private bool playAudio;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = false;
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDescription()
    {
        descriptionBox.GetComponentInChildren<Text>().text = itemHeld.GetComponent<ItemProperties>().itemDescription;

        if(isAudioDevice == true)
        {
            playerCharacter.GetComponent<ItemPickup>().givenAudio = itemHeld.GetComponent<ItemProperties>().recording;
            if(playerCharacter.GetComponent<ItemPickup>().playAudio == true)
            {
                playerCharacter.GetComponent<ItemPickup>().givenAudio.Play();
            }
        }
    }
}