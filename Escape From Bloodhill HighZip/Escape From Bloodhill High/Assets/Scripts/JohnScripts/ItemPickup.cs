using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool pickedUpItem;

    public GameObject[] inventory;
    public int inventoryCounter;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCounter = 0;
        pickedUpItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}