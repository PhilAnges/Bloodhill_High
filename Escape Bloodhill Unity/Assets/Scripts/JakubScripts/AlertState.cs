using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : AIState
{
    public AlertState(AIController parentAI)
    {
        parent = parentAI;
        maxAwareness = parent.alertTime;
    }

    public override void UpdateBehavior()
    {        
        parent.Sight();
        parent.Orient(parent.playerPosition);
        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.isStopped = true;
        parent.Orient(parent.playerPosition);
        Debug.Log("Entering Alert State");
    }

    public override void ExitBehavior()
    {
        parent.navAgent.isStopped = false;
    }

    public override void CheckConditions()
    {
        if (parent.gameController.playerIsSafe == true || parent.awareness <= 0f)
        {
            parent.SetState(new PatrolState(parent));
        }
        else
        {
            if (parent.awareness >= parent.alertTime)
            {
               parent.SetState(new SearchState(parent));
               Debug.Log("Entering Search State");
            }
        }
    }
}
