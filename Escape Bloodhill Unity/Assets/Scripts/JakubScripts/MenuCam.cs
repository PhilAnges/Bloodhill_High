using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCam : MonoBehaviour
{
    public Transform startPosition, playPosition, helpPosition, helpPosition2, quitPosition;
    public CamControl cam;
    public GameObject yes, no, confirmText, back;
    public int currentButton = 0;
    private MenuControl menuControl;
    public GameObject screenFade;
    public float sceneChangeDelay = 1f;
    public GameObject[] mainButtons;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamControl>();
        menuControl = GetComponent<MenuControl>();
    }

    public void PlayButton()
    {
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].SetActive(false);
        }
        cam.target = playPosition;
        currentButton = 1;
        back.SetActive(false);
        yes.SetActive(true);
        //no.GetComponentInChildren<Text>().text = "No";
        no.SetActive(true);
        confirmText.SetActive(true);

    }

    public void HelpButton()
    {
        cam.target = helpPosition;
        StartCoroutine("HelpZoom");
        currentButton = 2;
        back.SetActive(true);
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].SetActive(false);
        }
        yes.SetActive(false);
        //no.GetComponentInChildren<Text>().text = "Back";
        no.SetActive(false);
        //confirmText.SetActive(true);
    }

    public void QuitButton()
    {
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].SetActive(false);
        }
        cam.target = quitPosition;
        currentButton = 3;
        back.SetActive(false);
        yes.SetActive(true);
        //no.GetComponentInChildren<Text>().text = "No";
        no.SetActive(true);
        confirmText.SetActive(true);
    }

    public void ResetPosition()
    {
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].SetActive(true);
        }

        cam.target = startPosition;
        currentButton = 0;
        back.SetActive(false);
        yes.SetActive(false);
        //no.GetComponentInChildren<Text>().text = "No";
        no.SetActive(false);
        confirmText.SetActive(false);
    }

    public void BackFromHelp()
    {
        
        cam.target = helpPosition;
        StartCoroutine("ResetZoom");
        currentButton = 0;
        back.SetActive(false);
        yes.SetActive(false);
        //no.GetComponentInChildren<Text>().text = "No";
        no.SetActive(false);
        
    }

    public void YesButton()
    {
        switch (currentButton)
        {
            case 1:
                StartCoroutine("PlayScene");
                break;
            case 2:
                //StartCoroutine("HelpScene");
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

    IEnumerator HelpZoom()
    {
        yield return new WaitForSeconds(1f);
        cam.target = helpPosition2;
    }

    IEnumerator ResetZoom()
    {
        yield return new WaitForSeconds(1f);
        cam.target = startPosition;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].SetActive(true);
        }
        confirmText.SetActive(false);
    }
}
