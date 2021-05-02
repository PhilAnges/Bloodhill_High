using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoom : MonoBehaviour
{
    public GameObject player;
    public GameObject[] storedInventory;
    public GameObject currentPlayer;
    public Transform spawnPoint;
    public string playerTag;

    private bool isIn;
    private GameObject[] storage;
    private GameObject gameOverScreen;
    private GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
        storage = new GameObject[11];
        gameOverScreen = player.gameObject.GetComponent<PlayerHealth>().gameOverMenu;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        currentPlayer = GameObject.FindGameObjectWithTag(playerTag);
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isIn = true;
            storedInventory = StoredInventory(other.gameObject.GetComponent<ItemPickup>().inventory);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isIn = false;
        }
    }

    public void Respawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;

        player.gameObject.GetComponent<ItemPickup>().inventory = storedInventory;
        Destroy(playerCamera, 0.0f);
        
        Instantiate(player, spawnPoint.position, Quaternion.identity);
        Destroy(currentPlayer, 0.0f);

        //currentPlayer.GetComponentInChildren<Camera>().enabled = false;
        //player.GetComponentInChildren<Camera>().enabled = true;

        gameOverScreen.SetActive(false);

    }

    public GameObject[] StoredInventory(GameObject[] inventory)
    {
        if(isIn == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                storage[i] = inventory[i];
            }
        }
        return storage;
    }

    public GameObject[] RestoreInventory(GameObject[] inventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            storage[i] = inventory[i];
        }

        return storage;
    }
}