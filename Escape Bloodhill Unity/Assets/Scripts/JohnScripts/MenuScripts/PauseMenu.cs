using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseUI;
    public GameObject inventoryUI;
    public GameObject button;
    public KeyCode pauseButton;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        isPaused = false;
        pauseUI.SetActive(false);
        inventoryUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        button = GameObject.FindGameObjectWithTag("Button");

        player = GameObject.FindGameObjectWithTag("MainCamera");

        if ((Input.GetKeyUp(pauseButton)) && (isPaused == true))
        {
            //isPaused = !isPaused;
            Unpause();
        }else
        if ((Input.GetKeyUp(pauseButton)) && (isPaused == false))
        {
            //isPaused = !isPaused;
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
        Time.timeScale = 0f;
        inventoryUI.GetComponent<InventoryMenu>().DepopulateInventory(0);
    }

    public void Inventory()
    {
        pauseUI.SetActive(false);
        inventoryUI.SetActive(true);
        inventoryUI.GetComponent<InventoryMenu>().PopulateInventory(0);
    }

}