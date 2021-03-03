using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public RectTransform[] buttonLocations;
    public GameObject itemSlot;
    public GameObject playerCharacter;

    public GameObject descriptionBox;

    private int itemCounter;
    public GameObject[] itemHold;
    private GameObject holdButton;

    
    // Start is called before the first frame update
    void Start()
    {
        
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        itemCounter = 0;
        itemHold = playerCharacter.GetComponent<ItemPickup>().inventory;


    }

    // Update is called once per frame
    void Update()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        itemHold = playerCharacter.GetComponent<ItemPickup>().inventory;
    }

    public void PopulateInventory(int itemCounter)
    {
        itemHold = playerCharacter.GetComponent<ItemPickup>().inventory;
        while (itemHold[itemCounter] != null)
        {
            itemSlot.GetComponent<ShowItemDescription>().itemHeld = itemHold[itemCounter];
            itemSlot.GetComponent<ShowItemDescription>().descriptionBox = descriptionBox;
            itemSlot.GetComponentInChildren<Text>().text = itemHold[itemCounter].GetComponent<ItemProperties>().itemName;


            if (itemHold[itemCounter].CompareTag("AudioKey"))
            {
                itemSlot.GetComponent<ShowItemDescription>().isAudioDevice = true;
            }
            Instantiate(itemSlot, buttonLocations[itemCounter]);
            itemCounter++;
        }
    }

    public void DepopulateInventory(int itemCounter)
    {
        holdButton = buttonLocations[itemCounter].gameObject.GetComponentInChildren<Button>().gameObject;
        while (holdButton != null)
        {
            Destroy(holdButton, 0.0f);
            itemCounter++;
            holdButton = buttonLocations[itemCounter].gameObject.GetComponentInChildren<Button>().gameObject;
        }
            
    }
}