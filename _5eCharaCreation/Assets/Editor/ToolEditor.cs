using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ToolList))]
public class ToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Editor"))
            ToolEditorWindow.Open((ToolList)target);
    }
}
