using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState
{
    public PatrolState(AIController parentAI)
    {
        parent = parentAI;
    }

    public override void UpdateBehavior()
    {        
        parent.Sight();
        if (parent.navAgent.remainingDistance <= parent.arriveDistance)
        {
            if (parent.nextPoint == parent.pathPoints.Length - 1 && parent.pathDirection == 1)
            {
                parent.pathDirection *= -1;
            }
            else if (parent.nextPoint == 0 && parent.pathDirection == -1)
            {
                parent.pathDirection *= -1;
            }
            parent.previousPoint = parent.nextPoint;
            parent.nextPoint = parent.nextPoint + parent.pathDirection;
            parent.navAgent.SetDestination(parent.pathPoints[parent.nextPoint].position);
        }
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.SetDestination(parent.pathPoints[0].position);
        parent.navAgent.speed = parent.ogSpeed;
        parent.alertTime = parent.ogAlertTime;
        Debug.Log("Entering Patrol State");
    }

    public override void ExitBehavior()
    {

    }

    public override void CheckConditions()
    {
        if (parent.awareness > 0f)
        {
            parent.SetState(new AlertState(parent));
        }
    }
}
