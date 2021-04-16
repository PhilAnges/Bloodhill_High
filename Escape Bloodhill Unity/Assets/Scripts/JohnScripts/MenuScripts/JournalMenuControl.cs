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
        ShowJournalEntry(entry);
    }

    public void ShowJournalEntry(int entry)
    {
        descriptionBox.GetComponentInChildren<Text>().text = journalEntries[entry].GetComponent<ItemProperties>().itemDescription;
    }

    public void LeftArrow()
    {
        if(entry > 0)
        {
            entry = entry - 1;
            Debug.Log(entry);
        }
        //ShowJournalEntry(entry);
    }

    public void RightArrow()
    {
        if(entry < (journalEntries.Count - 1))
        {
            entry = entry + 1;
            Debug.Log(entry);
        }
        //ShowJournalEntry(entry);
    }
}
