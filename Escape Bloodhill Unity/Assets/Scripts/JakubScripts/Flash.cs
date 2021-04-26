using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private Light light;
    public float flashRate;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine("LightFlash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LightFlash()
    {
        light.enabled = false;
        yield return new WaitForSeconds(flashRate);
        light.enabled = true;
        yield return new WaitForSeconds(flashRate);
        StartCoroutine("LightFlash");
    }
}
