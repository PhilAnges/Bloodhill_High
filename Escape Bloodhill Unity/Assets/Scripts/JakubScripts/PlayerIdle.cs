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
        parent.cam.standHeight = Mathf.Lerp(parent.cam.standHeight, parent.cam.ogStandHeight, 0.1f);
        parent.cam.swayFactor = Mathf.Lerp(parent.cam.swayFactor, 0f, 0.05f);
        MoveDirection(parent.cam.child.moveMagnitude);
        parent.DrainStamina(false);
        parent.CalculateAdrenaline();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        parent.staminaRegenRate = parent.ogRegenRate;
        parent.isCrouching = false;
        parent.cam.standHeight = parent.cam.ogStandHeight;
        parent.moveSpeed = parent.ogMoveSpeed;
        Debug.Log("Entering Idle State");
        parent.rigbod.velocity = Vector3.zero;
        parent.noiseLevel = 0;
        parent.cam.child.moveDepth = 0f;
        parent.footsteps[0].Stop();
    }

    public override void ExitBehavior()
    {
        parent.moveSpeed = parent.ogMoveSpeed;
        //Debug.Log("Leaving Idle State");
    }

    public override void CheckConditions()
    {

        if (/*parent.airborn ||*/ parent.gameController.paused)
        {
            return;
        }
        

        if (Input.GetButtonDown("Crouch") && !parent.airborn)
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButton("Sprint") && !parent.airborn)
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
            parent.Flashlight(true);
        }
    }

    public override void FixedUpdateBehavior()
    {
        //parent.Move();
    }
}
