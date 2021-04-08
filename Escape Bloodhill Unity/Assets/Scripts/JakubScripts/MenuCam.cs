using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public Transform startPosition, playPosition, helpPosition, quitPosition;
    public CamControl cam;
    public GameObject yes, no;
    public int currentButton = 0;
    private MenuControl menuControl;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamControl>();
        menuControl = GetComponent<MenuControl>();
    }

    public void PlayButton()
    {
        cam.target = playPosition;
        currentButton = 1;
        yes.SetActive(true);
        no.SetActive(true);

    }

    public void HelpButton()
    {
        cam.target = helpPosition;
        currentButton = 2;
        yes.SetActive(true);
        no.SetActive(true);
    }

    public void QuitButton()
    {
        cam.target = quitPosition;
        currentButton = 3;
        yes.SetActive(true);
        no.SetActive(true);
    }

    public void ResetPosition()
    {
        cam.target = startPosition;
        currentButton = 0;
        yes.SetActive(false);
        no.SetActive(false);
    }

    public void YesButton()
    {
        switch (currentButton)
        {
            case 1:
                menuControl.ChangeScene(0);
                break;
            case 2:
                menuControl.ChangeScene(2);
                break;
            case 3:
                menuControl.ToExitGame();
                break;
        }
    }
}
