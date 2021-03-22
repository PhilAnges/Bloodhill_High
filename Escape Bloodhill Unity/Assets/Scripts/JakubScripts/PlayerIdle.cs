using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerController parentPlayer)
    {
        parent = parentPlayer;
    }

    public override void UpdateBehavior()
    {
        parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, parent.camera.ogStandHeight, 0.1f);
        parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.05f);
        parent.DrainStamina(false);
        parent.CalculateAdrenaline();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.isCrouching = false;
        parent.camera.standHeight = parent.camera.ogStandHeight;
        parent.moveSpeed = parent.ogMoveSpeed;
        Debug.Log("Entering Idle State");
        parent.rigbod.velocity = Vector3.zero;
        parent.noiseLevel = 0;
    }

    public override void ExitBehavior()
    {
        parent.moveSpeed = parent.ogMoveSpeed;
        Debug.Log("Leaving Idle State");
    }

    public override void CheckConditions()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButton("Sprint"))
        {
            parent.SetState(new PlayerRun(parent));
            return;
        }

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            parent.SetState(new PlayerWalk(parent));
            return;
        }

        if (Input.GetButtonDown("Flashlight"))
        {
            parent.Flashlight();
        }
    }
}
