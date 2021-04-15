using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalMenuControl : MonoBehaviour
{
    public GameObject descriptionBox;
    public List<GameObject> journalEntries;

    private int entry;

    // Start is called before the first frame update
    void Start()
    {
        entry = 0;
        //journalEntries = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowJournalEntry(int entry)
    {
        descriptionBox.GetComponentInChildren<Text>().text = journalEntries[entry].GetComponent<ItemProperties>().itemDescription;
    }

    public void LeftArrow()
    {
        entry = entry - 1;
        if(entry < 0)
        {
            entry = 0;
        }
        ShowJournalEntry(entry);
    }

    public void RightArrow()
    {
        entry = entry + 1;
        if(entry > journalEntries.Capacity)
        {
            entry = journalEntries.Capacity;
        }
        ShowJournalEntry(entry);
    }
}
