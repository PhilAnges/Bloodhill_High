using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public KeyCode pauseButton;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(pauseButton))
        {
            isPaused = !isPaused;
        }
        if (isPaused == true)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
        if (isPaused == false)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Unpause()
    {
        isPaused = false;
    }
}
