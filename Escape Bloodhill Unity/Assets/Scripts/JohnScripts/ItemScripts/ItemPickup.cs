using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool playAudio;

    public GameObject[] inventory;
    public AudioSource givenAudio;
    public GameObject relatedDoor;
    public int inventoryCounter;
    public bool pickedUpItem;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCounter = 0;
        pickedUpItem = false;
        relatedDoor = null;
        playAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] != null)
            {
                pickedUpItem = true;
            }
        }
    }
}