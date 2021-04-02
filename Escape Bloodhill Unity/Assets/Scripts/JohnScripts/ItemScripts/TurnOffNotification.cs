using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffNotification : MonoBehaviour
{
    public float duration;

    private float turnOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        turnOff = Time.time + duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy == true)
        {
            if(Time.time > turnOff)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
