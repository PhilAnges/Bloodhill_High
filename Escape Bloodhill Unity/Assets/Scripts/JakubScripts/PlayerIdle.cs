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
        Debug.Log("Entering Idle State");
        parent.noiseLevel = 0;
    }

    public override void ExitBehavior()
    {
        Debug.Log("Leaving Idle State");
    }

    public override void CheckConditions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }

        if (parent.GetXInput() != 0 || parent.GetZInput() != 0)
        {
            if (Input.GetAxis("Fire3") != 0)
            {
                parent.SetState(new PlayerRun(parent));
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
