using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;
    public float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
