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
        
    }   
}