﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemDescription : MonoBehaviour
{
    public GameObject itemHeld;
    public GameObject descriptionBox;
    public bool isAudioDevice;


    // Start is called before the first frame update
    void Start()
    {
        isAudioDevice = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDescription()
    {
        descriptionBox.GetComponentInChildren<Text>().text = itemHeld.GetComponent<ItemProperties>().itemDescription;
    }
}