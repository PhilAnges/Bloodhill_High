using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemDescription : MonoBehaviour
{
    public GameObject itemHeld;
    public GameObject descriptionBox;
    public bool isAudioDevice;

    private bool playAudio;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = false;
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
            //itemHeld.GetComponent<ItemProperties>().recording.Play();
            playAudio = true;
        }
    }
}