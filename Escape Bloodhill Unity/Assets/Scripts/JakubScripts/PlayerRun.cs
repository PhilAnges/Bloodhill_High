using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState
{
    public PlayerRun(PlayerController parentPlayer)
    {
        parent = parentPlayer;
    }

    public override void UpdateBehavior()
    {
        parent.camera.Look();
        parent.Move();
        parent.DrainStamina(true);
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        Debug.Log("Entering Run State");
        parent.moveSpeed *= parent.runSpeedMultiplier;
    }

    public override void ExitBehavior()
    {
        Debug.Log("Leaving Run State");
        parent.moveSpeed = parent.ogMoveSpeed;
    }

    public override void CheckConditions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Fire3") == 0)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                parent.SetState(new PlayerIdle(parent));
            }
            else
            {
                parent.SetState(new PlayerWalk(parent));
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            parent.Flashlight();
        }
    }
}
