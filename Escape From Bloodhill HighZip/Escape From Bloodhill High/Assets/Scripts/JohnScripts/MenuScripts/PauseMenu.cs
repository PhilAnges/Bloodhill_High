using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseUI;
    public KeyCode pauseButton;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        isPaused = false;
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(pauseButton))
        {
            isPaused = !isPaused;
            player.GetComponent<FirstPersonCamera>().enabled = !player.GetComponent<FirstPersonCamera>().enabled;
        }
        if (isPaused == true)
        {
            
            Cursor.lockState = CursorLockMode.None;
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
        if (isPaused == false)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Unpause()
    {
        isPaused = false;
        player.GetComponent<FirstPersonCamera>().enabled = true;
    }
}
