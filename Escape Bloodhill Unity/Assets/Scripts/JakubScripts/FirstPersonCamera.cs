using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private Vector2 mouseVector, smoothVector;

    [Range(0.1f, 10.0f)]
    public float sensitivity = 5.0f;
    [Range(0.1f, 5.0f)]
    public float smoothing = 2.0f;
    [Range(0.5f, 2f)]
    public float height = 0.1f;
    [Range(0.5f, 2f)]
    public float crouchHeight = 1f;
    [Range(0.5f, 2f)]
    public float standHeight = 1.5f;

    public PlayerController parent;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parent = transform.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (parent.isCrouching)
        {
            if (transform.position.y > crouchHeight)
            {
                Move(crouchHeight);
            }
        }
        else if (transform.position.y < standHeight)
        {
            Move(standHeight);
        }
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        
    }

    public void Look()
    {
        var mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseChange = Vector2.Scale(mouseChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseChange.x, 1f / smoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseChange.y, 1f / smoothing);
        mouseVector += smoothVector;
        mouseVector = new Vector2(mouseVector.x, Mathf.Clamp(mouseVector.y, -44, 60));

        transform.localRotation = Quaternion.AngleAxis(-mouseVector.y, Vector3.right);
        transform.parent.transform.localRotation = Quaternion.AngleAxis(mouseVector.x, transform.parent.transform.up);
    }

    public void Move(float targetHeight)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.075f);
    }
}