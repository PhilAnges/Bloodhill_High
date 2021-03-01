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
    }
}
