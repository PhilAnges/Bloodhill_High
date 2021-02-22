using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPatrolState : PatrolState
{
    public CamPatrolState(AIController parentAI) : base(parentAI)
    {
        parent = parentAI;
    }

    public override void UpdateBehavior()
    {        
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
            parent.nextPoint = parent.nextPoint + parent.pathDirection;
            parent.navAgent.SetDestination(parent.pathPoints[parent.nextPoint].position);
        }
    }   
}