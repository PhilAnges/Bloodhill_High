using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image blackScreen;
    private float alpha = 1f;
    public float fadeAmount = 0.05f;
    public float fadeInterval = 0.05f;
    public float fadeDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = GetComponentInChildren<Image>();
        StartCoroutine("Delay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UnFade()
    {
        if (alpha > 0)
        {
            alpha -= fadeAmount;
            blackScreen.color = new Vector4(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(fadeInterval);
            StartCoroutine("UnFade");
        }
        else
        {
            Destroy(this.gameObject);
            StopCoroutine("UnFade");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine("UnFade");
    }
}
