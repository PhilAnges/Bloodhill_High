using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState
{
    private bool loop = false;
    

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

        if (parent.pathPoints[parent.pathPoints.Count - 1].looped)
        {
            loop = true;
        }
        else
        {
            loop = false;
        }

        if (parent.navAgent.remainingDistance <= parent.arriveDistance)
        {
            if (!waiting)
            {
                waiting = true;
                parent.Wait(parent.currentPathPoint.waitTime);
            }

            if (parent.readyToMove)
            {
                if (loop)
                {
                    parent.nextPathPoint = parent.nextPathPoint.next;
                }
                else
                {
                    if (parent.nextPathPoint.next == null)
                    {
                        parent.pathDirection = -1;
                    }
                    else if (parent.nextPathPoint == parent.startPathPoint)
                    {
                        parent.pathDirection = 1;
                    }

                    if (parent.pathDirection == 1)
                    {
                        parent.nextPathPoint = parent.nextPathPoint.next;
                    }
                    else if (parent.pathDirection == -1)
                    {
                        parent.nextPathPoint = parent.nextPathPoint.prev;
                    }
                }

                parent.navAgent.SetDestination(parent.nextPathPoint.transform.position);
                waiting = false;
            }
        }

            
        else
        {
            parent.currentPathPoint = parent.nextPathPoint;
        }
        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.SetDestination(parent.nextPathPoint.transform.position);
        parent.aware = false;
        parent.awareness = 0f;
        parent.navAgent.speed = parent.patrolSpeed;
        parent.alertTime = parent.ogAlertTime;
        parent.eyeGlower.ResetEyes();
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
