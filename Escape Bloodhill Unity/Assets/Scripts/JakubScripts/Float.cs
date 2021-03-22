using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    public float magnitude;
    public Transform upper, lower;
    public bool up = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, upper.position, 0.05f);
        //transform.localPosition = upper.localPosition;
        
        if (up)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, upper.localPosition, 0.01f);
            if (Vector3.Distance(transform.localPosition, upper.localPosition) <= 0.02f)
            {
                up = false;
            }           
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, lower.localPosition, 0.01f);
            if (Vector3.Distance(transform.localPosition, lower.localPosition) <= 0.02f)
            {
                up = true;
            }
        }
        
    }

}
