using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalRotation : MonoBehaviour
{
    public float speed;

    public bool isRotating;
    public bool XZSpin;
    public bool YZSpin;
    public bool XYSpin;


    // Start is called before the first frame update
    void Start()
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating == true)
        {
            RotateTheObject();
        }
    }

    void RotateTheObject()
    {
        if (XZSpin == true)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        if (YZSpin == true)
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }
        if (XYSpin == true)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
