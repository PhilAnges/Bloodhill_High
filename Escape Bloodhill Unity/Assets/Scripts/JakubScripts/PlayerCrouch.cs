using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : PlayerState
{
    public PlayerCrouch(PlayerController parentPlayer)
    {
        parent = parentPlayer;
        beat = 1;
    }

    public override void UpdateBehavior()
    {
        parent.camera.Look();
        parent.Move();
        parent.DrainStamina(false);
        parent.CalculateAdrenaline();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        Debug.Log("Entering Crouch State");
        parent.moveSpeed *= parent.crouchSpeedMultiplier;
        parent.isCrouching = true;
        parent.ChangeSize();
        parent.camera.walkBobMagnitude = parent.crouchBobIntensity;
        parent.stepInterval = parent.crouchInterval;
        parent.camera.swayFactor = parent.crouchSwayIntensity;
        rythmTimer = parent.stepInterval * 2;
        highPoint = parent.camera.ogCrouchHeight;
        lowPoint = highPoint - parent.crouchBobIntensity;
        parent.noiseLevel = 1;
    }

    public override void ExitBehavior()
    {
        Debug.Log("Leaving Crouch State");
        parent.moveSpeed = parent.ogMoveSpeed;
        parent.camera.crouchHeight = highPoint;
        parent.isCrouching = false;
        parent.ChangeSize();
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

        if (Input.GetKeyDown(KeyCode.F))
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
                    parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, highPoint, 0.05f);
                    parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.05f);
                }
            }
            //Between Step 2 and Step 1
            else
            {
                parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, lowPoint, 0.1f);
                parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, parent.crouchSwayIntensity * beat, 0.1f);
            }
            rythmTimer -= Time.deltaTime;
            //parent.viewTimer = rythmTimer;
        }
        else
        {
            rythmTimer = parent.stepInterval * 2;
            //parent.viewTimer = rythmTimer;
            parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, parent.camera.ogCrouchHeight, 0.05f);
            parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.1f);
        }
    }
}
