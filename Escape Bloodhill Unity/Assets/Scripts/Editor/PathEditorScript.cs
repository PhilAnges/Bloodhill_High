using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path path = (Path)target;

        EditorGUILayout.HelpBox("This is a test", MessageType.Info);

        if (GUILayout.Button("Add Waypoint"))
        {
            path.AddWaypoint();
        }

        EditorGUILayout.HelpBox("Put this object where you want to start the path. Click 'Add Waypoint' to create the first point. " +
            "It will be selected automatically and has more inspector buttons to work with." +
            " There have to be at least two points for the path to work and they need to be its children." +
            " Start point is green, end point is red.", MessageType.Info);
    }
}
