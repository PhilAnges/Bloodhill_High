using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLights : MonoBehaviour
{
    public Light[] eyes;
    public Color startColor;
    public Color endColor;
    public AIController parent;
    public float percentAware;

    // Start is called before the first frame update
    void Awake()
    {
        parent = GetComponentInParent<AIController>();
        eyes = GetComponentsInChildren<Light>();
        startColor = eyes[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        percentAware = parent.awareness / parent.maxAwareness;
        endColor = new Color(1, 1 - percentAware, 0, 1);

        foreach (Light light in eyes)
        {
            light.color = Color.Lerp(light.color, endColor, 0.5f);
        }

    }
}
