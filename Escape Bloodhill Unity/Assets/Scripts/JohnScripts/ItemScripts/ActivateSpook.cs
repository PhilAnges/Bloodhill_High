using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpook : MonoBehaviour
{
    public string playerTag;

    public bool passiveSpook;
    public GameObject spook;
    public Vector3 spookSpawn;
    public Quaternion spookFacing;

    public KeyCode pickItUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (passiveSpook == true)
            {
                Instantiate(spook, spookSpawn, spookFacing);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            
            if(passiveSpook == false)
            {
                if (Input.GetKeyUp(pickItUp))
                {
                    Instantiate(spook, spookSpawn, spookFacing);
                }
            }
        }
    }
}
