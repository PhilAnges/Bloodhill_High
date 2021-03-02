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
    private GameObject[] itemHold;
    private GameObject holdButton;
    //public List<GameObject> buttons;

    private bool testing;
    // Start is called before the first frame update
    void Start()
    {
        testing = false;
        //buttons = new List<GameObject>();

        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        itemCounter = 0;

        for(int i = 0; i < buttonLocations.Length; i++)
        {
            itemHold[i] = null;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        itemHold = playerCharacter.GetComponent<ItemPickup>().inventory;
    }

    public void PopulateInventory(int itemCounter)
    {
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
            //buttons.Add(itemSlot);
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