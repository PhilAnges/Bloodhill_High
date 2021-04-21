using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPath : MonoBehaviour
{
    public Path newPath;
    public bool teleport = true;
    private GameController gameController;
    public bool playerOnly;
    public int phaseReq;
    public int phaseTrigger;
    public bool makeIdle;
    public bool tauntIdle;

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

        if (other.tag == "Player" && gameController.basementPhase == phaseReq)
        {
            AIController enemy = gameController.enemyScript;
            enemy.SetPath(newPath);
            enemy.Teleport(enemy.currentPath.transform.position);
            gameController.basementPhase = phaseTrigger;
            //enemy.awareness = 0f;
            //enemy.aware = false;
            if (!makeIdle)
            {
                enemy.SetState(new PatrolState(enemy));
            }
            else if (tauntIdle)
            {
                enemy.SetState(new IdleTaunt(enemy));
            }
            else
            {
                enemy.SetState(new IdleState(enemy));
            }

            
        }
    }
}
