using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BypassDoor : MonoBehaviour
{
    public GameObject otherDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(otherDoor.GetComponent<OpenDoor>().openTheDoor == true)
        {
            gameObject.GetComponent<OpenDoor>().openTheDoor = true;
        }
    }
}
