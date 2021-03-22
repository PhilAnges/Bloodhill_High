using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool playerIsSafe = false;
    public bool playerIsHidden = false;
    public GameObject playerPrefab;
    public Transform player;

    void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }

        
    }

    void Update()
    {
        
    }
}
