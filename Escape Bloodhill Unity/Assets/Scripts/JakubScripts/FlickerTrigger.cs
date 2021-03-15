using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerTrigger : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.FlashlightFlicker();
        }
    }
}
