using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCount : MonoBehaviour
{
    private Text fps;

    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int frames = (int)(1f / Time.unscaledDeltaTime);
        fps.text = frames.ToString();
    }
}
