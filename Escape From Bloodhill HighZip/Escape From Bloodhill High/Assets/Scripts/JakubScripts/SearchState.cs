using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : AIState
{
    private float checkTime = 3f;
    private float checkTimer = 0;
    private bool coastIsClear = false;
    private bool checking = false;

    public SearchState(AIController parentAI)
    {
        parent = parentAI;        
    }

    public override void UpdateBehavior()
    {
        parent.Sight();
        if (parent.navAgent.remainingDistance <= 0 && checking == false)
        {
            Quaternion targetRot = Quaternion.LookRotation((parent.transform.position + (parent.playerDirection.normalized * 5f)) - parent.transform.position);
            parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, targetRot, 5f * Time.deltaTime);
            checking = true;
            checkTimer = checkTime;
            Debug.Log("Setting timer");
        }
        if (checkTimer >= 0f)
        {
            //Debug.Log("Updating timer");
            checkTimer -= Time.deltaTime;
        }
        if (checkTimer <= 0 && checking == true && parent.aware == false)
        {
            Debug.Log("Setting bool");
            coastIsClear = true;
        }
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.navAgent.SetDestination(parent.playerPosition);
        parent.awareness = 0f;
        if (parent.alertTime >= 0)
        {
            parent.alertTime--;
        }
        Debug.Log("Entering Search State");
    }

    public override void ExitBehavior()
    {
        
    }

    public override void CheckConditions()
    {
        if (parent.aware)
        {
            if (parent.awareness >= parent.alertTime)
            {
                parent.SetState(new ChaseState(parent));
            }
            else
            {
                parent.navAgent.SetDestination(parent.playerPosition);
            }
        }
        else if(coastIsClear)
        {
            parent.SetState(new PatrolState(parent));
        }
    }
}