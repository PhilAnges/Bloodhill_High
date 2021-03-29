using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerState
{

    public PlayerWalk(PlayerController parentPlayer)
    {
        parent = parentPlayer;
        beat = 1;
    }

    public override void UpdateBehavior()
    {
        parent.DrainStamina(false);
        parent.CalculateAdrenaline();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        //Debug.Log("Entering Walk State");
        parent.staminaRegenRate *= parent.staminaRegenMultiplier;
        parent.stepInterval = parent.ogstepInterval;
        rythmTimer = parent.stepInterval * 2;
        parent.camera.walkBobMagnitude = parent.camera.ogMagnitude;
        highPoint = parent.camera.ogStandHeight;
        lowPoint = highPoint- parent.camera.walkBobMagnitude;
        parent.noiseLevel = 2;
    }

    public override void ExitBehavior()
    {
        //Debug.Log("Leaving Walk State");
        parent.staminaRegenRate = parent.ogRegenRate;
        parent.camera.standHeight = highPoint;
    }

    public override void CheckConditions()
    {

        if (Input.GetButtonDown("Crouch") && !parent.airborn)
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }
        if (Input.GetButton("Sprint") && Input.GetAxisRaw("Vertical") > 0 && !parent.airborn)
        {
            parent.SetState(new PlayerRun(parent));
            return;
        }
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            parent.SetState(new PlayerIdle(parent));
            return;
        }

        if (Input.GetButtonDown("Flashlight"))
        {
            parent.Flashlight();
        }
    }

    public override void WalkRythm()
    {
        if (parent.GetXInput() != 0 || parent.GetZInput() != 0)
        {
            //If its halfway down to zero or more
            if (rythmTimer <= parent.stepInterval)
            {
                //Step2 / Switch Foot (cam high)
                if (rythmTimer <= 0 && step)
                {
                    rythmTimer = parent.stepInterval * 2;
                    step = false;
                }
                else if (rythmTimer > 0)
                {
                    //Step1 / Place Foot (cam low)
                    if (!step)
                    {
                        beat *= -1;
                        step = true;
                    }
                    //Between Step 1 and Step 2
                    parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, highPoint, 0.1f);
                    parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.05f);
                }
            }
            //Between Step 2 and Step 1
            else
            {
                parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, lowPoint, 0.1f);
                parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, parent.walkSwayIntensity * beat, 0.1f);
            }
            rythmTimer -= Time.deltaTime;
        }
        else
        {
            rythmTimer = parent.stepInterval * 2;
            parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, parent.camera.ogStandHeight, 0.1f);
            parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.1f);
        }
    }

    public override void FixedUpdateBehavior()
    {
        parent.Move();
    }
}
