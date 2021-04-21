using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour
{
    public AIController parent;
    private Transform player;

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        //transform.LookAt(targetPosition, transform.up);

        if (parent.aware)
        {
            transform.LookAt(targetPosition, transform.up);
        }

    }
}

