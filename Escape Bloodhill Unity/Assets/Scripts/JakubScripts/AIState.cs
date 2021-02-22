using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState
{
    public AIController parent;
    public AIState nextState;
    public float maxAwareness;


    public AIState()
    {
        
    }

    public virtual void UpdateBehavior()
    {
        
    }

    public virtual void EntryBehavior()
    {

    }

    public virtual void ExitBehavior()
    {

    }

    public virtual void CheckConditions()
    {

    }    
}