using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillEditorWindow : ExtendedEditorWindow
{
    
    public static void Open(SkillList _skillList)
    {
        SkillEditorWindow window = GetWindow<SkillEditorWindow>("Skill Editor");
        window.serializedObject = new SerializedObject(_skillList);

    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            EditorGUILayout.LabelField("No Skill List Selected");
            return;
        }

        currentProperty = serializedObject.FindProperty("content");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSideBar(currentProperty);
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
        DrawField("linkedAtribute", true);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(5);
        DrawField("description", true);
        EditorGUILayout.EndVertical();




    }
}
