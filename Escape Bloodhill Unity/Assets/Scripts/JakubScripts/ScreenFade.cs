using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    private Image blackScreen;
    private float alpha = 1f;
    public float fadeAmount = 0.05f;
    public float fadeInterval = 5f;
    public float fadeDelay = 1f;
    private bool fade = false;
    private Color targetColor;

    public bool fadeIn = false;

    public Image blackFix;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeIn)
        {
            blackScreen = GetComponentInChildren<Image>();
            blackScreen.color = new Color(0f, 0f, 0f, 0f);
            targetColor = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            blackScreen = GetComponentInChildren<Image>();
            blackScreen.color = new Color(0f, 0f, 0f, 1f);
            targetColor = new Color(0f, 0f, 0f, 0f);
        }
     
        StartCoroutine("Delay");
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            if (blackScreen.color == targetColor)
            {
                Destroy(this.gameObject);
            }

            blackScreen.color = Color.Lerp(blackScreen.color, targetColor, fadeInterval * Time.deltaTime);
        }

        if (fadeIn && blackScreen.color.a >= 0.99f)
        {
            blackFix.gameObject.SetActive(true);
        }
    }

    IEnumerator UnFade()
    {
        if (fadeIn && alpha >= 0.5f)
        {
            
        }

        if (alpha > 0)
        {
            alpha -= fadeAmount;
            blackScreen.color = new Vector4(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(fadeInterval);
            StartCoroutine("UnFade");
        }
        else
        {
            
            //Destroy(this.gameObject);
            StopCoroutine("UnFade");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(fadeDelay);
        fade = true;
        //StartCoroutine("UnFade");
    }
}
