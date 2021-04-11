using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTrigger : MonoBehaviour
{
    private PlayerController player;
    private AIController enemy;
    private GameController gameController;
    public Path path;
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
        if (other.tag == "Player")
        {
            player = other.GetComponent<PlayerController>();
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIController>();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && player.CheckForItem() == true && hasSpawned == false)
        {
            enemy.SetPath(path);
            enemy.Teleport(enemy.currentPath.transform.position);
            gameController.basementPhase = 3;
            enemy.SetState(new IdleState(enemy));
            hasSpawned = true;
        }
    }
}
