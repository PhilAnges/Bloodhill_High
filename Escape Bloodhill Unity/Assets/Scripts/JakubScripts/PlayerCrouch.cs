using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : PlayerState
{
    public PlayerCrouch(PlayerController parentPlayer)
    {
        parent = parentPlayer;
    }

    public override void UpdateBehavior()
    {
        parent.camera.Look();
        parent.Move();
        parent.DrainStamina(false);
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        Debug.Log("Entering Crouch State");
        parent.moveSpeed *= parent.crouchSpeedMultiplier;
        parent.isCrouching = true;
    }

    public override void ExitBehavior()
    {
        Debug.Log("Leaving Crouch State");
        parent.moveSpeed = parent.ogMoveSpeed;
        parent.isCrouching = false;
    }

    public override void CheckConditions()
    {
        if (parent.GetXInput() != 0 || parent.GetZInput() != 0)
        {
            if (Input.GetAxis("Fire3") != 0)
            {
                parent.SetState(new PlayerRun(parent));
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                parent.SetState(new PlayerWalk(parent));
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            parent.SetState(new PlayerIdle(parent));
        }
    }
}
