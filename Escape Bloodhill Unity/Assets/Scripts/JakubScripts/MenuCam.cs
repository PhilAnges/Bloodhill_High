using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public Transform startPosition, playPosition, helpPosition, quitPosition;
    public CamControl cam;
    public int currentButton = 0;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamControl>();
    }

    public void PlayButton()
    {
        cam.target = playPosition;
        currentButton = 1;
    }

    public void HelpButton()
    {
        cam.target = helpPosition;
        currentButton = 2;
    }

    public void QuitButton()
    {
        cam.target = quitPosition;
        currentButton = 3;
    }

    public void ResetPosition()
    {
        cam.target = startPosition;
        currentButton = 0;
    }
}
