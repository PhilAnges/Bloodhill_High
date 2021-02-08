﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOverToPickUp : MonoBehaviour
{

    public string playerTag;
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {

            pickedUpText.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
