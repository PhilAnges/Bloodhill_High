using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    public string playerTag;
    public float howLong;
    public float spin;
    public KeyCode actionKey;
    public bool locked;

    public float upperLimRotate;
    public float lowerLimRotate;

    private Vector3 outOfWay;
    public Collider destroyThis;
    private float tolerance;
    public bool openTheDoor;
    private float waitToMove;

    public AudioSource openDoorSound;
    public AudioSource lockedDoorSound;
    public GameObject notification;


    // Start is called before the first frame update
    void Start()
    {
        outOfWay = new Vector3(transform.position.x, transform.position.y, transform.position.z + .04f);
        openTheDoor = false;
        tolerance = howLong * Time.deltaTime;
        notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.rotation.x);
        if ((transform.position != outOfWay) && (openTheDoor == true))
        {
            OpenTheDoor();
            SwingOpen();
        }
        /*if(openTheDoor == true)
        {
            SwingOpen();
        }
        if ((transform.rotation. >= ))
        {
            openTheDoor = false;
        }*/
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            notification.SetActive(true);
            if ((Input.GetKeyUp(actionKey)) && (locked == false))
            {
                Destroy(destroyThis);
                openDoorSound.Play();
                openTheDoor = true;
            }
            else
            if (other.GetComponent<ItemPickup>().relatedDoor == gameObject)
            {
                Destroy(destroyThis);
                openDoorSound.Play();
                openTheDoor = true;
            }
        }
    }
    public void OpenTheDoor()
    {
        Vector3 headingTo = outOfWay - transform.position;
        transform.position += (headingTo / headingTo.magnitude) * howLong * Time.deltaTime;
        if (headingTo.magnitude < tolerance)
        {
            transform.position = outOfWay;
            waitToMove = Time.deltaTime;
        }
    }
    public void SwingOpen()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, swing, spin);
        transform.Rotate(Vector3.up * spin * Time.deltaTime);

    }
}