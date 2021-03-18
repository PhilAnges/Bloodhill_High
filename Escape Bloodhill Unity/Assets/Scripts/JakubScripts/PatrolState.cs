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
        //Delete this line in final build
        parent.navAgent.speed = parent.patrolSpeed;

        parent.Sight();

        if (parent.pathPoints.Count < 2)
        {
            Debug.Log("AI is trying to use a path with less than two points");
            return;
        }

        if (parent.navAgent.remainingDistance <= parent.arriveDistance)
        {
            if (parent.nextPoint == parent.pathPoints.Count - 1 && parent.pathDirection == 1)
            {
                parent.pathDirection *= -1;
            }
            else if (parent.nextPoint == 0 && parent.pathDirection == -1)
            {
                parent.pathDirection *= -1;
            }
            parent.previousPoint = parent.nextPoint;
            parent.nextPoint = parent.nextPoint + parent.pathDirection;
            parent.navAgent.SetDestination(parent.pathPoints[parent.nextPoint].gameObject.transform.position);
        }
        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.SetDestination(parent.pathPoints[0].gameObject.transform.position);
        parent.navAgent.speed = parent.patrolSpeed;
        parent.alertTime = parent.ogAlertTime;
        Debug.Log("Entering Patrol State");
    }

    public override void ExitBehavior()
    {

    }

    public override void CheckConditions()
    {
        if (parent.aware)
        {
            parent.SetState(new AlertState(parent));
        }
    }
}
