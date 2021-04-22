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

    public virtual void FixedUpdateBehavior()
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

    public virtual void MoveDirection(float magnitude)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            parent.cam.child.moveDepth =  -magnitude;
            //Debug.Log(parent.camera.child.moveDepth);
        }
        if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("LookBack") != 0)
        {
            parent.cam.child.moveDepth = magnitude;
            //Debug.Log(parent.camera.child.moveDepth);
        }
        if (Input.GetAxis("LookBack") != 0)
        {
            parent.cam.child.moveDepth = parent.cam.child.runMoveMagnitude;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            parent.cam.child.moveDepth = 0f;
        }
    }
    
}