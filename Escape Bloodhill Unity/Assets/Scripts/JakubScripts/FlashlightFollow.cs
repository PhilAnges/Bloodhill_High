using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform parent;
    public float swayFactor = 0;
    public float farBackness = 0;


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);

        //targetPosition = Quaternion.Euler(0f, parent.transform.rotation.y, 0f) * targetPosition;


        transform.rotation = parent.transform.rotation * Quaternion.Euler(90, 0, 1);
        transform.position = Vector3.Lerp(transform.position, targetPosition + (parent.transform.right * 0.5f) - (parent.transform.forward * -0.5f) - (parent.transform.up * 0.3f), 0.4f);
        //transform.rotation = parent.transform.rotation * Quaternion.Euler(90, 0, 1);
    }
}
