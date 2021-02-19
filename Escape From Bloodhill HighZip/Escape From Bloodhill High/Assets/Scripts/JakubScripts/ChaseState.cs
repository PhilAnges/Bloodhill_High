using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    public ChaseState(AIController parentAI)
    {
        parent = parentAI;
    }

    public override void UpdateBehavior()
    {        
        parent.Sight();
        parent.Orient(parent.playerPosition);
        parent.navAgent.SetDestination(parent.playerPosition);
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.stoppingDistance = parent.playerChaseStoppingDistance;
        parent.navAgent.SetDestination(parent.playerPosition);
        parent.navAgent.speed = parent.patrolSpeed * parent.chaseSpeedMultiplier;
        Debug.Log("Entering Chase State");
    }

    public override void ExitBehavior()
    {
        parent.navAgent.stoppingDistance = parent.ogStoppingDistance;
        parent.Teleport(parent.pathPoints[parent.previousPoint].position);
    }

    public override void CheckConditions()
    {
        if (parent.gameController.playerIsSafe == true)
        {
            parent.SetState(new PatrolState(parent));
        }     
    }
}
