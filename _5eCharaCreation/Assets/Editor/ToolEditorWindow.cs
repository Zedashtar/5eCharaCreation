using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolEditorWindow : ExtendedEditorWindow
{
    Vector2 scrollPos;

    public static void Open(ToolList _toolList)
    {
        ToolEditorWindow window = GetWindow<ToolEditorWindow>("Tool Editor");
        window.serializedObject = new SerializedObject(_toolList);

    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            EditorGUILayout.LabelField("No Tool List Selected");
            return;
        }

        currentProperty = serializedObject.FindProperty("content");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
        DrawSideBar(currentProperty);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            DrawSelectedPropertiesPanel();
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();

    }

    void DrawSelectedPropertiesPanel()
    {
        currentProperty = selectedProperty;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();

        DrawField("name", true);
        EditorGUILayout.Space(5);
        DrawField("type", true);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(5);
        DrawField("description", true);
        EditorGUILayout.EndVertical();
    }
}
