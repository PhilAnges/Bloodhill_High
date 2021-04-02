using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyTrigger : MonoBehaviour
{
    private GameController gameController;
    private bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasSpawned && !gameController.enemyActive)
        {
            hasSpawned = true;
            gameController.SpawnEnemy();
        }
    }
}
