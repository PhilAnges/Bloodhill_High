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
    public AudioPassword isAudioDevice;

    private int itemCounter;
    public GameObject[] itemHold;

    private bool testing;
    // Start is called before the first frame update
    void Start()
    {
        testing = false;

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
        itemHold = playerCharacter.GetComponent<ItemPickup>().inventory;
        if (itemHold[itemCounter] != null)
        {
            itemSlot.GetComponent<ShowItemDescription>().itemHeld = itemHold[itemCounter];
            itemSlot.GetComponent<ShowItemDescription>().descriptionBox = descriptionBox;
            itemSlot.GetComponentInChildren<Text>().text = itemHold[itemCounter].GetComponent<ItemProperties>().itemName;

            if(itemHold[itemCounter].TryGetComponent(out AudioPassword component))
            {
                Debug.Log("Work");
                isAudioDevice = component;
            }
            

            if (testing == true)
            {
                itemSlot.GetComponent<ShowItemDescription>().isAudioDevice = true;

                //isAudioDevice = itemHold[itemCounter].GetComponent<AudioPassword>();
                Debug.Log(testing);
            }

            //descriptionBox.GetComponentInChildren<Text>().text = itemHold[itemCounter].GetComponent<ItemProperties>().itemDescription;
            Instantiate(itemSlot, buttonLocations[itemCounter]);
            itemCounter++;
        }
    }
}