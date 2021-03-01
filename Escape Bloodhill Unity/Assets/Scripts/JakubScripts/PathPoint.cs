using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathPoint prev, next;
    public bool endPoint = false;
    public bool looped = false;

    void Start()
    {

    }
}
