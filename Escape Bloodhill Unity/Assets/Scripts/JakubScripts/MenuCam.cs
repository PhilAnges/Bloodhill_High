using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public Transform startPosition, playPosition, helpPosition, quitPosition;
    public CamControl cam;
    public GameObject yes, no, confirmText;
    public int currentButton = 0;
    private MenuControl menuControl;
    public GameObject screenFade;
    public float sceneChangeDelay = 1f;


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
        confirmText.SetActive(true);

    }

    public void HelpButton()
    {
        cam.target = helpPosition;
        currentButton = 2;
        yes.SetActive(true);
        no.SetActive(true);
        confirmText.SetActive(true);
    }

    public void QuitButton()
    {
        cam.target = quitPosition;
        currentButton = 3;
        yes.SetActive(true);
        no.SetActive(true);
        confirmText.SetActive(true);
    }

    public void ResetPosition()
    {
        cam.target = startPosition;
        currentButton = 0;
        yes.SetActive(false);
        no.SetActive(false);
        confirmText.SetActive(false);
    }

    public void YesButton()
    {
        switch (currentButton)
        {
            case 1:
                StartCoroutine("PlayScene");
                break;
            case 2:
                StartCoroutine("HelpScene");
                break;
            case 3:
                StartCoroutine("QuitGame");
                break;
        }
    }

    IEnumerator PlayScene()
    {
        Instantiate(screenFade);
        yield return new WaitForSeconds(sceneChangeDelay);
        menuControl.ChangeScene(0);
    }
    IEnumerator HelpScene()
    {
        Instantiate(screenFade);
        yield return new WaitForSeconds(sceneChangeDelay);
        menuControl.ChangeScene(2);
    }
    IEnumerator QuitGame()
    {
        Instantiate(screenFade);
        yield return new WaitForSeconds(sceneChangeDelay);
        menuControl.ToExitGame();
    }
}
