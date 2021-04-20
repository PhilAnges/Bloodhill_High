﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonPickup : MonoBehaviour
{
    public string playerTag;
    public KeyCode pickItUp;
    public AudioSource pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKey(pickItUp))
            {
                while (other.gameObject.GetComponent<ItemPickup>().inventory[other.gameObject.GetComponent<ItemPickup>().inventoryCounter] != null)
                {
                    other.gameObject.GetComponent<ItemPickup>().inventoryCounter++;
                }
                other.gameObject.GetComponent<ItemPickup>().inventory[other.gameObject.GetComponent<ItemPickup>().inventoryCounter] = gameObject;

                gameObject.SetActive(false);
                pickUpSound.Play();
            }
        }
    }
}