using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState
{
    

    public PlayerRun(PlayerController parentPlayer)
    {
        parent = parentPlayer;
        beat = 1;
    }

    public override void UpdateBehavior()
    {
        //parent.camera.Look();
        //parent.Move();
        MoveDirection(parent.camera.child.runMoveMagnitude);
        parent.DrainStamina(true);
        parent.CalculateAdrenaline();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        //Debug.Log("Entering Run State");
        parent.staminaRegenRate *= parent.staminaRegenMultiplier;
        parent.moveSpeed *= parent.runSpeedMultiplier;
        parent.stepInterval = parent.runInterval;
        rythmTimer = parent.stepInterval * 2;
        highPoint = parent.camera.ogStandHeight;
        lowPoint = highPoint - parent.runBobIntensity;
        parent.noiseLevel = 3;
        parent.running = true;
        parent.footsteps[0].Stop();
        parent.footsteps[2].Play();
    }

    public override void ExitBehavior()
    {
        //Debug.Log("Leaving Run State");
        parent.moveSpeed = parent.ogMoveSpeed;
        
        parent.camera.standHeight = highPoint;
        parent.running = false;
        parent.footsteps[2].Stop();
    }

    public override void CheckConditions()
    {
        /*if (Input.GetButtonDown("Crouch") && !parent.airborn)
        {
            parent.SetState(new PlayerCrouch(parent));
            return;
        }*/
        if (parent.gameController.paused)
        {
            parent.SetState(new PlayerIdle(parent));
            return;
        }


        if (!Input.GetButton("Sprint"))
        {
            if (!Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
            {
                parent.SetState(new PlayerIdle(parent));
                return;
            }
            else
            {
                parent.SetState(new PlayerWalk(parent));
                return;
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0.9)
        {
            if (!Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
            {
                parent.SetState(new PlayerIdle(parent));
                return;
            }
            else
            {
                parent.SetState(new PlayerWalk(parent));
                return;
            }
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
                    parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, highPoint, 0.5f);
                    parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.05f);
                    parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, parent.camera.child.runBobMagnitude, 0.1f);
                    parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, parent.camera.child.runTilt, 0.1f);
                }
            }
            //Between Step 2 and Step 1
            else
            {
                parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, lowPoint, 0.5f);
                parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, parent.runSwayIntensity * beat, 0.1f);
                parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, -parent.camera.child.runBobMagnitude, 0.1f);
                parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, -parent.camera.child.runTilt, 0.1f);
            }
            rythmTimer -= Time.deltaTime;
            //parent.viewTimer = rythmTimer;
        }
        else
        {
            rythmTimer = parent.stepInterval * 2;
            //parent.viewTimer = rythmTimer;
            parent.camera.standHeight = Mathf.Lerp(parent.camera.standHeight, parent.camera.ogStandHeight, 0.1f);
            parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.1f);
            parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, 0f, 0.1f);
            parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, 0f, 0.1f);
        }

        //parent.viewStep = step;
    }

    public override void FixedUpdateBehavior()
    {
        parent.Move();
    }

    
}
