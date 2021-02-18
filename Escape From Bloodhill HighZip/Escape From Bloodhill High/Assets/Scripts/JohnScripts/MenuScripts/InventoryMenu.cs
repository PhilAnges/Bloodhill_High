using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public RectTransform[] buttonLocations;
    public GameObject itemSlot;
    public GameObject playerCharacter;

    private int itemCounter;
    public GameObject[] itemHold;
    // Start is called before the first frame update
    void Start()
    {
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
            Instantiate(itemSlot, buttonLocations[itemCounter]);
            itemCounter++;
        }
    }
}