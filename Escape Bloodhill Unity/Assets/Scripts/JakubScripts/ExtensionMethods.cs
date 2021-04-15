using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Fade(this AudioSource clip, float fadeSpeed)
    {
        if (clip.volume > 0.01)
        {
            clip.volume = Mathf.Lerp(clip.volume, 0f, fadeSpeed * Time.deltaTime);
            Fade(clip, fadeSpeed);
        }
    }
}
