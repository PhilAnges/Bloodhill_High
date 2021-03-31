using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public IdleState(AIController parentAI)
    {
        parent = parentAI;
    }
    public override void UpdateBehavior()
    {
        //Delete this line in final build
        parent.navAgent.speed = parent.patrolSpeed;
        parent.Sight();  
        UpdateAwareness();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.aware = false;
        parent.awareness = 0f;
        parent.navAgent.speed = parent.patrolSpeed;
        parent.alertTime = parent.ogAlertTime;
        parent.eyeGlower.ResetEyes();
        Debug.Log("Entering Idle State");
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
