using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(PathPoint))]
public class PathPointEditorScript : Editor
{
    private Path parent;
#if (UNITY_EDITOR)
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PathPoint pathPoint = (PathPoint)target;
        parent = pathPoint.gameObject.GetComponentInParent<Path>();


        if (GUILayout.Button("Add Waypoint"))
        {
            parent.AddWaypoint();
        }

        if (GUILayout.Button("Insert Next"))
        {
            if (/*pathPoint.endPoint == true*/pathPoint.transform.GetSiblingIndex() == parent.transform.childCount - 1)
            {
                Debug.Log("Use 'Add Waypoint' to add to the end of the path");
            }
            else
            {
                parent.InsertWaypoint(pathPoint.transform.GetSiblingIndex() + 1, pathPoint.transform.position);
            }
            
        }
        if (GUILayout.Button("Insert Prev"))
        {
            if (pathPoint.transform.GetSiblingIndex() == 0)
            {
                Debug.Log("Can't insert a new start node");
            }
            else
            {
                parent.InsertWaypoint(pathPoint.transform.GetSiblingIndex(), pathPoint.transform.position);
            }            
        }
        if (GUILayout.Button("Loop"))
        {
            if (pathPoint.looped)
            {
                Debug.Log("This node already loops back to the starting point");
            }
            else
            {
                if (pathPoint.transform.GetSiblingIndex() == parent.transform.childCount - 1)
                {
                    pathPoint.next = parent.transform.GetChild(0).GetComponent<PathPoint>();
                    parent.transform.GetChild(0).GetComponent<PathPoint>().prev = pathPoint;
                    pathPoint.looped = true;
                }
                else
                {
                    Debug.Log("You can only create a loop from the last point on the path");
                }
            }   
        }
        if (GUILayout.Button("Unloop"))
        {
            if (pathPoint.looped)
            {
                pathPoint.next = null;
                parent.transform.GetChild(0).GetComponent<PathPoint>().prev = null;
                pathPoint.looped = false;
            }
            else
            {
                Debug.Log("This path point doesn't loop back to the start");
            }

            
        }

        if (GUILayout.Button("Delete This Waypoint"))
        {
            if (pathPoint.transform.GetSiblingIndex() == parent.transform.childCount - 1)
            {
                parent.lastPointMade = pathPoint.prev;
                //pathPoint.prev.endPoint = true;
                DestroyImmediate(pathPoint.gameObject);
            }
            else if (pathPoint.transform.GetSiblingIndex() == 0)
            {
                pathPoint.next.prev = null;
                DestroyImmediate(pathPoint.gameObject);
            }
            else
            {
                pathPoint.prev.next = pathPoint.next;
                pathPoint.next.prev = pathPoint.prev;
                DestroyImmediate(pathPoint.gameObject);
            }
        }

        EditorGUILayout.HelpBox("'Add Waypoint' will create a new point at the end of the path. \n" + 
            "'Insert Next' will add a point in between this point and the next. \n" + 
            "'Insert Prev' will add a new point between this point and the previous one. \n" +
            "'Loop' will link this point to the first one. Only works on the last point and takes a few seconds to show visually in the scene. \n" +
            "'Unloop' will unlink the first node from this one. Also takes a few seconds to show in the scene. \n" +
            "Use 'Delete this waypoint' to delete points or you'll have to relink the ones surrounding them manually. \n", MessageType.Info);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy |GizmoType.NonSelected |GizmoType.Pickable)]
#endif
    static void DrawPointNode(PathPoint point, GizmoType gizmoType)
    {

        if (point.transform.GetSiblingIndex() == point.transform.parent.transform.childCount - 1 && point.transform.GetSiblingIndex() != 0)
        {
            Gizmos.color = Color.red;
        }
        else if (point.transform.GetSiblingIndex() == 0)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawSphere(point.gameObject.transform.position, 0.5f);

        if (point.next != null)
        {
            Handles.DrawDottedLine(point.transform.position, point.next.transform.position, 3f);
        }
    }
}
