using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonPickup : MonoBehaviour
{
    public string playerTag;
    public KeyCode pickItUp;
    public AudioSource pickUpSound;
    public GameObject notification;

    // Start is called before the first frame update
    void Start()
    {
        notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            notification.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            notification.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKey(pickItUp))
            {
                notification.SetActive(false);
                while (other.gameObject.GetComponent<ItemPickup>().inventory[other.gameObject.GetComponent<ItemPickup>().inventoryCounter] != null)
                {
                    other.gameObject.GetComponent<ItemPickup>().inventoryCounter++;
                }
                other.gameObject.GetComponent<ItemPickup>().inventory[other.gameObject.GetComponent<ItemPickup>().inventoryCounter] = gameObject;

                gameObject.SetActive(false);
                pickUpSound.Play();
            }
        }
    }
}