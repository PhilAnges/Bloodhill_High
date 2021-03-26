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
        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.stoppingDistance = parent.playerChaseStoppingDistance;
        parent.navAgent.SetDestination(parent.playerPosition);
        parent.navAgent.speed = parent.patrolSpeed * parent.chaseSpeedMultiplier;
        parent.player.isBeingChased = true;
        Debug.Log("Entering Chase State");
    }

    public override void ExitBehavior()
    {
        parent.navAgent.stoppingDistance = parent.ogStoppingDistance;
        parent.player.isBeingChased = false;
    }

    public override void CheckConditions()
    {
        if (parent.player.hp.currentHealth == 0)
        {
            parent.Teleport(parent.nextPathPoint.transform.position);
            parent.SetState(new PatrolState(parent));
            return;
        }

        if (parent.player.isHidden == true)
        {
            parent.SetState(new PatrolState(parent));
            return;
        }
        if (parent.awareness <= 0)
        {
            parent.SetState(new SearchState(parent));
        }

       
    }
}
