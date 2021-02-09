﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonPickup : MonoBehaviour
{
    public string playerTag;
    public KeyCode pickItUp;
    public GameObject pickedUpText;

    // Start is called before the first frame update
    void Start()
    {
        pickedUpText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(pickItUp))
            {
                other.gameObject.GetComponent<ItemPickup>().pickedUpItem = true;
                pickedUpText.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
