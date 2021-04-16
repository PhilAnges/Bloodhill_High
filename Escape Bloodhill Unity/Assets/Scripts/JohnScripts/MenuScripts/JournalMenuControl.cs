using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalMenuControl : MonoBehaviour
{
    public GameObject descriptionBox;
    public GameObject leftButton;
    public GameObject rightButton;
    public List<GameObject> journalEntries;
    public Texture journalStart;
    public Texture journalMiddle;

    private int entry;

    // Start is called before the first frame update
    void Start()
    {
        entry = 0;
        descriptionBox.GetComponent<RawImage>().texture = journalStart;
        //journalEntries = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowJournalEntry(entry);
        if(entry == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
            descriptionBox.GetComponent<RawImage>().texture = journalStart;
        }
        if(entry == (journalEntries.Count - 1))
        {
            leftButton.SetActive(true);
            rightButton.SetActive(false);
            descriptionBox.GetComponent<RawImage>().texture = journalMiddle;
        }
        if((entry > 0) && (entry < (journalEntries.Count - 1)))
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            descriptionBox.GetComponent<RawImage>().texture = journalMiddle;
        }

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
