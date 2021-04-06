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
        if (Vector3.Distance(parent.transform.position, parent.playerPosition) <= 3.5f)
        {
            parent.animator.SetBool("attack", true);
        }
        else
        {
            parent.animator.SetBool("attack", false);
        }

        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.stoppingDistance = parent.playerChaseStoppingDistance;
        parent.navAgent.SetDestination(parent.playerPosition);
        parent.navAgent.speed = parent.patrolSpeed * parent.chaseSpeedMultiplier;
        parent.player.isBeingChased = true;
        parent.gameController.ChangeMusic(2, parent.gameController.volumes[2]);
        parent.animator.SetBool("chasing", true);
        Debug.Log("Entering Chase State");
    }

    public override void ExitBehavior()
    {
        parent.navAgent.stoppingDistance = parent.ogStoppingDistance;
        parent.player.isBeingChased = false;
        parent.animator.SetBool("chasing", false);
        parent.animator.SetBool("attack", false);
        parent.gameController.ChangeMusic(0, parent.gameController.volumes[0]);
    }

    public override void CheckConditions()
    {
        if (parent.player.hp.currentHealth == 0)
        {
            parent.Teleport(parent.nextPathPoint.transform.position);
            if (parent.pathPoints.Count < 2)
            {
                parent.SetState(new IdleState(parent));
            }
            else
            {
                parent.SetState(new PatrolState(parent));
            }
            return;
        }

        if (parent.gameController.playerIsSafe)
        {
            if (parent.pathPoints.Count < 2)
            {
                parent.SetState(new IdleState(parent));
            }
            else
            {
                parent.SetState(new PatrolState(parent));
            }
            return;
        }
        if (parent.awareness <= 0)
        {
            parent.SetState(new SearchState(parent));
        }

       
    }
}
