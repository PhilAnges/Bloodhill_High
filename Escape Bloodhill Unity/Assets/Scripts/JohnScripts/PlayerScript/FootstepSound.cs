using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource walking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            walking.Play();
        }        
        if (Input.GetKeyDown(KeyCode.A))
        {
            walking.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            walking.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            walking.Play();
        }
        if (Input.anyKey == false)
        {
            walking.Stop();
        }
    }
}
