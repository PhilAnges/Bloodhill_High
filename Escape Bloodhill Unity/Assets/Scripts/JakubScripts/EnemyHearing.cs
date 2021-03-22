using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    public bool inRange = false;
    public Vector3 playerPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPos = other.transform.position;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPos = other.transform.position;
            inRange = true;
        }
    }


}
