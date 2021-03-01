using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public string playerTag;
    public int heal;

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
            other.gameObject.GetComponent<PlayerHealth>().currentHealth = other.gameObject.GetComponent<PlayerHealth>().currentHealth + heal;
            gameObject.SetActive(false);
        }
    }
}
