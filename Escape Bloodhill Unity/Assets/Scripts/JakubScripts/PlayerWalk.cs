using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerState
{
    public PlayerWalk(PlayerController parentPlayer)
    {
        parent = parentPlayer;
    }

    public override void UpdateBehavior()
    {
        //parent.camera.Look();
        parent.Move();
        parent.DrainStamina(false);
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        Debug.Log("Entering Walk State");
        parent.staminaRegenRate *= parent.staminaRegenMultiplier;
    }

    public override void ExitBehavior()
    {
        Debug.Log("Leaving Walk State");
        parent.staminaRegenRate = parent.ogRegenRate;
    }

    public override void CheckConditions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            if (Input.GetAxis("Vertical") == 0)
            {
                parent.SetState(new PlayerIdle(parent));
            }
            else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Fire3") != 0)
            {
                parent.SetState(new PlayerRun(parent));
            }  
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            parent.Flashlight();
        }
    }
}
