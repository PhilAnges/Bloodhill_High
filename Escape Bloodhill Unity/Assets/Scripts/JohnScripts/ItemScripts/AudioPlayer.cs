using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    
    public string playerTag;

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
        other.gameObject.GetComponent<ItemPickup>().playAudio = true;
    }
    public void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<ItemPickup>().playAudio = false;
    }
}