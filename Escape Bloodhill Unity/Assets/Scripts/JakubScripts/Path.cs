using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject waypointPrefab;
    private Vector3 spawnPosition;

    public PathPoint lastPointMade;


    public void AddWaypoint()
    {
        if (lastPointMade != null)
        {
            spawnPosition = lastPointMade.transform.position;
        }
        else
        {
            if (transform.childCount > 0)
            {
                Debug.Log(transform.childCount);
                lastPointMade = transform.GetChild(transform.childCount - 1).GetComponent<PathPoint>();
                spawnPosition = lastPointMade.transform.position;
            }
            else
            {
                spawnPosition = transform.position;
            }        
        }
        
        GameObject newPoint = Instantiate(waypointPrefab, spawnPosition, Quaternion.identity, this.transform);
        PathPoint newPointScript = newPoint.GetComponent<PathPoint>();

        if (lastPointMade != null)
        {
            newPointScript.prev = lastPointMade;
            lastPointMade.next = newPointScript;
            lastPointMade.endPoint = false;
            newPointScript.endPoint = true;
        }
       
        lastPointMade = newPointScript;
    }

    public void InsertWaypoint(int index, Vector3 position)
    {
        spawnPosition = position;
        GameObject newPoint = Instantiate(waypointPrefab, spawnPosition, Quaternion.identity, this.transform);
        PathPoint newPointScript = newPoint.GetComponent<PathPoint>();

        newPointScript.transform.SetSiblingIndex(index);

        newPointScript.prev = transform.GetChild(index - 1).GetComponent<PathPoint>();
        newPointScript.next = transform.GetChild(index + 1).GetComponent<PathPoint>();

        newPointScript.prev.next = newPointScript;
        newPointScript.next.prev = newPointScript;


    }
}
