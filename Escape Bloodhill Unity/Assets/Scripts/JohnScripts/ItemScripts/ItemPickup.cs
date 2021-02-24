﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool playAudio;

    public GameObject[] inventory;
    public AudioSource givenAudio;
    public int inventoryCounter;
    public bool pickedUpItem;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCounter = 0;
        pickedUpItem = false;
        playAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}