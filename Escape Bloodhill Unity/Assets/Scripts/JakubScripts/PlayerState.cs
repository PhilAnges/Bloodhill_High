using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public PlayerController parent;
    public PlayerState nextState;

    public float rythmTimer;
    public float lowPoint;
    public float highPoint;
    public int beat;
    public bool step = false;

    public PlayerState()
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

    public virtual void WalkRythm()
    {

    }
}