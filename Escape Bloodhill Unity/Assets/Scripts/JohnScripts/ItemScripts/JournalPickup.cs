using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalPickup : MonoBehaviour
{

    public int journalNumber;
    public bool mustPickUp;

    public string playerTag;
    public KeyCode pickItUp;
    public GameObject journalButton;
    public GameObject journalMenuControler;

    public GameObject notification;
    public AudioSource journalPickupNoise;

    // Start is called before the first frame update
    void Start()
    {
        notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (mustPickUp == true) {
                if (Input.GetKeyUp(pickItUp))
                {
                    PickupTheJournal();
                }
            }
            else
            if(mustPickUp == false){
                PickupTheJournal();
            }
        }
    }

    public void PickupTheJournal()
    {
        if (journalNumber == journalMenuControler.GetComponent<JournalMenuControl>().journalEntries.Count)
        {
            journalButton.GetComponentInChildren<Text>().text = gameObject.GetComponent<ItemProperties>().itemName;
            journalMenuControler.GetComponent<JournalMenuControl>().journalEntries.Add(gameObject);

            notification.SetActive(true);
            journalPickupNoise.Play();
            gameObject.SetActive(false);
        }
    }
}
