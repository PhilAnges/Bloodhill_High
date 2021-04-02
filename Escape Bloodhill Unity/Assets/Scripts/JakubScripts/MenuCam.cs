using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public Transform playPosition, optionsPosition, quitPosition;
    public bool moving = false;
    public Transform target;
    public float lookSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, lookSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
        }
    }
}
