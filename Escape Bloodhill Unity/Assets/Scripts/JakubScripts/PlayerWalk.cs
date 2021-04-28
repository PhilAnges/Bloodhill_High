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
        //parent.Move();
        MoveDirection(parent.cam.child.moveMagnitude);
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
        parent.cam.walkBobMagnitude = parent.cam.ogMagnitude;
        highPoint = parent.cam.ogStandHeight;
        lowPoint = highPoint- parent.cam.walkBobMagnitude;
        parent.noiseLevel = 2;
        parent.cam.child.moveDepth = parent.cam.child.moveMagnitude;
        parent.footsteps[0].Stop();
        parent.footsteps[1].PlayDelayed(0.2f);
    }

    public override void ExitBehavior()
    {
        //Debug.Log("Leaving Walk State");
        parent.staminaRegenRate = parent.ogRegenRate;
        parent.cam.standHeight = highPoint;
        parent.cam.child.bobHeight = 0f;
        parent.footsteps[1].Stop();

    }

    public override void CheckConditions()
    {

        if (parent.gameController.paused)
        {
            parent.SetState(new PlayerIdle(parent));
            return;
        }

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
            parent.Flashlight(true);
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
                    parent.cam.standHeight = Mathf.Lerp(parent.cam.standHeight, highPoint, 0.1f);
                    parent.cam.swayFactor = Mathf.Lerp(parent.cam.swayFactor, 0f, 0.05f);
                    parent.cam.child.bobHeight = Mathf.Lerp(parent.cam.child.bobHeight, parent.cam.child.bobMagnitude, 0.1f);
                    parent.cam.child.moveTilt = Mathf.Lerp(parent.cam.child.moveTilt, parent.cam.child.walkTilt, 0.1f);
                }
            }
            //Between Step 2 and Step 1
            else
            {
                parent.cam.standHeight = Mathf.Lerp(parent.cam.standHeight, lowPoint, 0.1f);
                parent.cam.swayFactor = Mathf.Lerp(parent.cam.swayFactor, parent.walkSwayIntensity * beat, 0.1f);
                parent.cam.child.bobHeight = Mathf.Lerp(parent.cam.child.bobHeight, -parent.cam.child.bobMagnitude, 0.1f);
                parent.cam.child.moveTilt = Mathf.Lerp(parent.cam.child.moveTilt, -parent.cam.child.walkTilt, 0.1f);
            }
            rythmTimer -= Time.deltaTime;
        }
        else
        {
            rythmTimer = parent.stepInterval * 2;
            parent.cam.standHeight = Mathf.Lerp(parent.cam.standHeight, parent.cam.ogStandHeight, 0.1f);
            parent.cam.swayFactor = Mathf.Lerp(parent.cam.swayFactor, 0f, 0.1f);
            parent.cam.child.bobHeight = Mathf.Lerp(parent.cam.child.bobHeight, 0f, 0.1f);
            parent.cam.child.moveTilt = Mathf.Lerp(parent.cam.child.moveTilt, 0f, 0.1f);
        }
    }

    public override void FixedUpdateBehavior()
    {
        parent.Move();
    }

}
