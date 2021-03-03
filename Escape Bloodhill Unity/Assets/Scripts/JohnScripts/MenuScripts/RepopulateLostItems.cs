using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepopulateLostItems : MonoBehaviour
{

    public ItemProperties[] allItems;
    public GameObject[] itemsInSaferoom;

    // Start is called before the first frame update
    void Start()
    {
        allItems = FindObjectsOfType<ItemProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        itemsInSaferoom = gameObject.GetComponent<SafeRoom>().storedInventory;

    }

    public void Repopulate()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            for (int j = 0; j < itemsInSaferoom.Length; j++)
            {
                if (allItems[i].gameObject == itemsInSaferoom[j])
                {
                    allItems[i].gameObject.SetActive(false);
                    //Debug.Log("Item Stored");
                    break;
                }
                else
                {
                    allItems[i].gameObject.SetActive(true);
                    //Debug.Log("Item not stored");
                }
            }
        }
    }
}
