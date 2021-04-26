using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditSequence : MonoBehaviour
{

    public float initialDelay = 1f;
    public float musicStartTime = 0f;
    public float titleStartTime = 1f;
    public float titleFadeInTime = 10f;
    public float titleFadeOutTime = 10f;
    public float titleStayTime;


    public GameObject creditController;
    public RawImage title;

    private Color transparent, opaque;


    private AudioSource music;
    private int fade = 0;

    public float titleFadeInSpeed = 5f;
    public float titleFadeOutSpeed = 5f;
    private bool inSpeedChanged = false;
    private bool outSpeedChanged = false;


    // Start is called before the first frame update
    void Start()
    {
        transparent = new Color(1f, 1f, 1f, 0f);
        opaque = new Color(1f, 1f, 1f, 1f);
        title.color = transparent;
        music = GetComponent<AudioSource>();
        StartCoroutine("CreditsSequence");
    }

    // Update is called once per frame
    void Update()
    {
        switch (fade)
        {
            case 0:
                break;
            case 1:
                //FadeTitleIn();
                StartCoroutine("FadeTitleIn");
                break;
            case 2:
                //FadeTitleOut();
                StartCoroutine("FadeTitleOut");
                break;
        }
    }

    IEnumerator CreditsSequence()
    {
        yield return new WaitForSeconds(initialDelay);
        //yield return new WaitForSeconds(musicStartTime);
        music.Play();

        yield return new WaitForSeconds(titleStartTime);
        fade = 1;

        yield return new WaitForSeconds(titleFadeInTime);

        //Start Title Fade Out
        fade = 2;

        yield return new WaitForSeconds(titleStayTime);

        yield return new WaitForSeconds(titleFadeOutTime);

        creditController.SetActive(true);
    }

    IEnumerator  FadeTitleIn()
    {
        if (title.color != opaque)
        {
            if (title.color.a > 0.3f && !inSpeedChanged)
            {
                titleFadeInSpeed *= 2f;
                inSpeedChanged = true;
            }


            title.color = new Color(1f, 1f, 1f, title.color.a + titleFadeInSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        /*
        if (title.color != opaque)
        {
            title.color = Color.Lerp(title.color, opaque, titleFadeInSpeed * Time.deltaTime);
        }
        */
    }
    IEnumerator FadeTitleOut()
    {
        if (title.color != transparent)
        {
            if (title.color.a < 0.2f && !outSpeedChanged)
            {
                titleFadeOutSpeed /= 2f;
                outSpeedChanged = true;
            }

            title.color = new Color(1f, 1f, 1f, title.color.a - titleFadeOutSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    /*
    private void FadeTitleOut()
    {
        if (title.color != transparent)
        {
            title.color = Color.Lerp(title.color, transparent, titleFadeOutSpeed * Time.deltaTime);
        }
    }
    */
}
