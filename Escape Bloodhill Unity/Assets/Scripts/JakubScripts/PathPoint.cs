using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathPoint prev, next;
    public bool looped = false;
    public float waitTime = 0;
    public Transform lookTarget;

    void Start()
    {

    }
}
