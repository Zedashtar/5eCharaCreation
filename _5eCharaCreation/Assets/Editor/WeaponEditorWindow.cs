using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponEditorWindow : ExtendedEditorWindow
{
    Vector2 scrollPos;

    public static void Open(WeaponList _weaponList)
    {
        WeaponEditorWindow window = GetWindow<WeaponEditorWindow>("Weapon Editor");
        window.serializedObject = new SerializedObject(_weaponList);

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
        DrawField("name", true);
        DrawField("damageDice", true);
        DrawField("damageType", true);
        DrawField("range", true);
        DrawField("type", true);
        DrawField("properties", true);
        EditorGUILayout.EndVertical();

    }
}
