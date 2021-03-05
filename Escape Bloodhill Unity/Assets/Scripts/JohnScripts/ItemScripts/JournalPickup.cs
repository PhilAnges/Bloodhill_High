using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalPickup : MonoBehaviour
{
    public string playerTag;
    public KeyCode pickItUp;
    public GameObject journalButton;
    public GameObject journalMenuControler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(pickItUp))
            {
                journalButton.GetComponentInChildren<Text>().text = gameObject.GetComponent<ItemProperties>().itemName;
                journalMenuControler.GetComponent<JournalMenuControl>().journalEntries.Add(gameObject);
                gameObject.SetActive(false);
                
            }
        }
    }
}
