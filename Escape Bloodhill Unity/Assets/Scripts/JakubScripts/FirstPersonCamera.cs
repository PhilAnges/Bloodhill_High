using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private Vector2 mouseVector, smoothVector;
    private GameObject player;
    private Rigidbody rigBod;
    private Vector3 vel;

    [Range(0.1f, 10.0f)]
    public float sensitivity = 5.0f;
    [Range(0.1f, 5.0f)]
    public float smoothing = 2.0f;
    [Range(1f, 10f)]
    public float moveSpeed = 2f;
    [Range(0.5f, 2f)]
    public float height = 0.1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        //rigBod = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseChange = Vector2.Scale(mouseChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseChange.x, 1f / smoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseChange.y, 1f / smoothing);
        mouseVector += smoothVector;
        mouseVector = new Vector2(mouseVector.x, Mathf.Clamp(mouseVector.y, -44, 60));

        transform.localRotation = Quaternion.AngleAxis(-mouseVector.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, player.transform.up);
        //transform.localRotation = Quaternion.AngleAxis(-mouseVector.y, transform.right) * player.transform.rotation;
        //transform.rotation = Quaternion.AngleAxis(mouseVector.x, player.transform.up);


        player.transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed * Time.deltaTime);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z);
        //vel = player.transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //rigBod.velocity = vel;
    }

    private void LateUpdate()
    {
        
    }
}