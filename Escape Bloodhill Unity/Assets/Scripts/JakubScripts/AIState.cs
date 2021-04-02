﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState
{
    public AIController parent;
    public AIState nextState;
    public float maxAwareness;
    public bool waiting = false;

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

    public void UpdateAwareness()
    {
        if (parent.aware && parent.awareness < parent.maxAwareness)
        {
            parent.awareness += Time.deltaTime;
            parent.playerLostTime = 0f;
        }
        else if (parent.playerLostTime < parent.timeToLosePlayer)
        {
            parent.playerLostTime += Time.deltaTime;
        }

        if (parent.awareness > 0 && parent.playerLostTime >= parent.timeToLosePlayer)
        {
            parent.awareness -= Time.deltaTime;
        }
    }
}