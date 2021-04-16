using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTaunt: AIState
{
    public IdleTaunt(AIController parentAI)
    {
        parent = parentAI;
    }
    public override void UpdateBehavior()
    {
        //Delete this line in final build
        parent.navAgent.speed = parent.patrolSpeed;

        if (parent.navAgent.remainingDistance == 0f && parent.startPathPoint.lookTarget != null)
        {
            parent.Orient(parent.startPathPoint.lookTarget.position);
        }

        //parent.Sight();  
        //UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.aware = false;
        parent.awareness = 0f;
        parent.navAgent.speed = parent.patrolSpeed;
        parent.alertTime = parent.ogAlertTime;
        parent.eyeGlower.ResetEyes();
        //parent.navAgent.SetDestination(parent.startPathPoint.transform.position);
        parent.animator.SetBool("taunting", true);
        Debug.Log("Entering Idle State");
    }

    public override void ExitBehavior()
    {
        parent.animator.SetBool("taunting", false);
    }

    public override void CheckConditions()
    {

    }
}
