using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseUI;
    public GameObject inventoryUI;
    public GameObject journalUI;
    public GameObject button;
    public KeyCode pauseButton;
    public KeyCode inventoryButton;
    public KeyCode journalButton;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        

        isPaused = false;
        pauseUI.SetActive(false);
        inventoryUI.SetActive(false);
        journalUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        button = GameObject.FindGameObjectWithTag("Button");

        player = GameObject.FindGameObjectWithTag("MainCamera");

        if (Input.GetKeyUp(inventoryButton))
        {
            player.GetComponent<FirstPersonCamera>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Inventory();
        }

        if (Input.GetKeyUp(journalButton))
        {
            player.GetComponent<FirstPersonCamera>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Journal();
        }

        if ((Input.GetKeyUp(pauseButton)) && (isPaused == true))
        {
            Unpause();
        }else
        if ((Input.GetKeyUp(pauseButton)) && (isPaused == false))
        {
            Pause();
        }
    }

    public void Unpause()
    {
        isPaused = false;
        player.GetComponent<FirstPersonCamera>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        inventoryUI.SetActive(false);
        pauseUI.SetActive(false);
        journalUI.SetActive(false);
        Time.timeScale = 1f;
        inventoryUI.GetComponent<InventoryMenu>().DepopulateInventory(0);
    }

    public void Pause()
    {
        isPaused = true;
        player.GetComponent<FirstPersonCamera>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        inventoryUI.SetActive(false);
        journalUI.SetActive(false);
        Time.timeScale = 0f;
        inventoryUI.GetComponent<InventoryMenu>().DepopulateInventory(0);
    }

    public void Inventory()
    {
        pauseUI.SetActive(false);
        journalUI.SetActive(false);
        inventoryUI.SetActive(true);
        inventoryUI.GetComponent<InventoryMenu>().PopulateInventory(0);
    }

    public void Journal()
    {
        inventoryUI.SetActive(false);
        journalUI.SetActive(true);
        inventoryUI.GetComponent<InventoryMenu>().DepopulateInventory(0);
    }

}