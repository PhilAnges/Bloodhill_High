using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoom : MonoBehaviour
{
    public GameObject player;
    public GameObject currentPlayer;
    public Transform spawnPoint;
    public string playerTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = GameObject.FindGameObjectWithTag(playerTag);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.gameObject;


        }
    }

    public void Respawn()
    {
        
        

        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
         
        //currentPlayer.GetComponentInChildren<Camera>().enabled = false;
        //Destroy(currentPlayer, 0.0f);
        Instantiate(player, spawnPoint);
        //player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponent<PlayerHealth>().gameOverMenu.SetActive(false);

        

    }

}
