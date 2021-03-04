using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public PlayerController parent;
    public PlayerState nextState;

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
}