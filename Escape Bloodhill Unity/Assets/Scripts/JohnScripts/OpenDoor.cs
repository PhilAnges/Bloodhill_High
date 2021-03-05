using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public string playerTag;
    public float howLong;
    public float spin;

    private Vector3 outOfWay;
    public Quaternion swing;
    private float tolerance;
    private bool openTheDoor;
    private float waitToMove;


    // Start is called before the first frame update
    void Start()
    {
        outOfWay = new Vector3(transform.position.x, transform.position.y, transform.position.z + .04f);
        openTheDoor = false;
        tolerance = howLong * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position != outOfWay) && (openTheDoor == true))
        {
            OpenTheDoor();
            SwingOpen();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                
                openTheDoor = true;
            }
        }
    }

    void OpenTheDoor()
    {
        Vector3 headingTo = outOfWay - transform.position;
        transform.position += (headingTo / headingTo.magnitude) * howLong * Time.deltaTime;
        if (headingTo.magnitude < tolerance)
        {
            transform.position = outOfWay;
            waitToMove = Time.deltaTime;
        }
    }
    void SwingOpen()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, swing, spin);
    }
}