using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalMenuControl : MonoBehaviour
{
    public GameObject descriptionBox;
    public List<GameObject> journalEntries;

    // Start is called before the first frame update
    void Start()
    {
        journalEntries = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowJournalEntry(int entry)
    {
        descriptionBox.GetComponentInChildren<Text>().text = journalEntries[entry].GetComponent<ItemProperties>().itemDescription;
    }
}
