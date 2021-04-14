using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicState
{
    public AudioSource audioTrack;

    public MusicState(AudioSource track)
    {
        audioTrack = track;
    }

    public virtual void UpdateBehavior()
    {

    }

    public virtual void FixedUpdateBehavior()
    {

    }

    public virtual void EntryBehavior(bool fade)
    {
        if (fade)
        {

        }
        else
        {

        }
    }

    public virtual void ExitBehavior(bool fade)
    {
        if (fade)
        {

        }
        else
        {

        }
    }

    public virtual void CheckConditions()
    {

    }
}