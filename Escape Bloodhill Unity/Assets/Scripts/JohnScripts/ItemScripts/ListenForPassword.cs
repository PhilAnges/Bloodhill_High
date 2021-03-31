using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForPassword : MonoBehaviour
{
    public string playerTag;
    public JournalPickup pickUpScript;
    private int twoStepPlan;

    // Start is called before the first frame update
    void Start()
    {
        twoStepPlan = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if ((twoStepPlan == 0) && (other.gameObject.GetComponent<ItemPickup>().givenAudio.isPlaying == true))
            {
                twoStepPlan = 1;
            }
            if ((twoStepPlan == 1) && (other.gameObject.GetComponent<ItemPickup>().givenAudio.isPlaying == false))
            {
                pickUpScript.PickupTheJournal();
            }
        }
    }

}