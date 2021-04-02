using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public Transform startPosition, playPosition, helpPosition, quitPosition;
    public CamControl cam;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamControl>();
    }

    public void PlayButton()
    {
        cam.target = playPosition;
    }

    public void HelpButton()
    {
        cam.target = helpPosition;
    }

    public void QuitButton()
    {
        cam.target = quitPosition;
    }

    public void ResetPosition()
    {
        cam.target = startPosition;
    }
}
