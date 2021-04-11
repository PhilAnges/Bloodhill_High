using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime : MonoBehaviour
{
    public string playerTag;
    public KeyCode stop;
    private bool stopTime;
    // Start is called before the first frame update
    void Start()
    {
        stopTime = false;        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(stop))
        {
            Debug.Log(stopTime);
            Debug.Log(Time.timeScale);
            if(Time.timeScale == 0f)
            {
                stopTime = false;
            }
        }

           
        if(stopTime == true)
        {
            Time.timeScale = 0f;
        }
        if(stopTime == false)
        {
            Time.timeScale = 1f;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(stop))
            {
                stopTime = true;
            }
        }
    }
}