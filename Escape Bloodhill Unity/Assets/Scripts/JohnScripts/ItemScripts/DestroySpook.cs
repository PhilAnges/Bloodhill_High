using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpook : MonoBehaviour
{
    public float timeToScare;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToScare);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
