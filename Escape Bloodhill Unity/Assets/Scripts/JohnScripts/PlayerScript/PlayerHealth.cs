using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            gameOverMenu.SetActive(true);
        }
    }
}
