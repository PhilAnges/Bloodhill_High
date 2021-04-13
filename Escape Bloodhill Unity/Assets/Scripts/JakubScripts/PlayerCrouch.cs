using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : PlayerState
{
    public bool headRoom;
    private int mask;
    private bool soundPlayed = false;

    public PlayerCrouch(PlayerController parentPlayer)
    {
        parent = parentPlayer;
        beat = 1;
        mask = ~LayerMask.GetMask("Player");
    }

    public override void UpdateBehavior()
    {
        //parent.camera.Look();
        //parent.Move();
        parent.DrainStamina(false);
        parent.CalculateAdrenaline();
        CheckHeadroom();
        CheckConditions();
    }

    public override void EntryBehavior()
    {
        //Debug.Log("Entering Crouch State");
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
        headRoom = true;
        
    }

    public override void ExitBehavior()
    {
        //Debug.Log("Leaving Crouch State");
        parent.moveSpeed = parent.ogMoveSpeed;
        parent.camera.crouchHeight = highPoint;
        parent.isCrouching = false;
        parent.ChangeSize();
        parent.footsteps[0].Stop();
    }

    public override void CheckConditions()
    {
        if (parent.airborn)
        {
            parent.SetState(new PlayerIdle(parent));
            return;
        }


        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)/* && headRoom*/)
        {
            if (headRoom)
            {
                if (Input.GetButton("Sprint"))
                {
                    parent.SetState(new PlayerRun(parent));
                }
                else if (Input.GetButtonDown("Crouch"))
                {
                    parent.SetState(new PlayerWalk(parent));
                }
            }
            if (!soundPlayed)
            {
                parent.footsteps[0].PlayDelayed(0.2f);
                soundPlayed = true;
            }
            
        }
        else if (soundPlayed)
        {
            parent.footsteps[0].Stop();
            soundPlayed = false;
        }
        else if (Input.GetButtonDown("Crouch") && headRoom)
        {
            parent.SetState(new PlayerIdle(parent));
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
                    parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, highPoint, 0.05f);
                    parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.05f);
                    parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, parent.camera.child.crouchBobMagnitude, 0.1f);
                    parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, parent.camera.child.crouchTilt, 0.1f);
                }
            }
            //Between Step 2 and Step 1
            else
            {
                parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, lowPoint, 0.1f);
                parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, parent.crouchSwayIntensity * beat, 0.1f);
                parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, -parent.camera.child.crouchBobMagnitude, 0.1f);
                parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, -parent.camera.child.crouchTilt, 0.1f);
            }
            rythmTimer -= Time.deltaTime;
        }
        else
        {
            rythmTimer = parent.stepInterval * 2;
            parent.camera.crouchHeight = Mathf.Lerp(parent.camera.crouchHeight, parent.camera.ogCrouchHeight, 0.05f);
            parent.camera.swayFactor = Mathf.Lerp(parent.camera.swayFactor, 0f, 0.1f);
            parent.camera.child.bobHeight = Mathf.Lerp(parent.camera.child.bobHeight, 0f, 0.1f);
            parent.camera.child.moveTilt = Mathf.Lerp(parent.camera.child.moveTilt, 0f, 0.1f);
        }
    }

    public override void FixedUpdateBehavior()
    {
        parent.Move();
    }

    public void CheckHeadroom()
    {
        RaycastHit hit;

        Debug.DrawRay(parent.transform.position + parent.transform.up * 0.05f, parent.transform.up * 2f, Color.blue, 0.5f);
        if (Physics.SphereCast(parent.transform.position + parent.transform.up * 0.05f, 0.1f, parent.transform.up, out hit, 2f, mask))
        {
            headRoom = false;
            //Debug.Log(hit.collider.gameObject);
        }
        else
        {
            headRoom = true;
        }
    }
}
