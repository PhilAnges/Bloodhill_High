using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAnim : MonoBehaviour
{
    public OpenDoor doorScript;
    public Animator lockAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = GetComponent<OpenDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorScript.openTheDoor)
        {
            lockAnimator.SetTrigger("unlock");
        }
    }
}
