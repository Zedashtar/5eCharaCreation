using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceID, int line)
    {
        Class classObj = EditorUtility.InstanceIDToObject(instanceID) as Class;
        if (classObj != null)
        {
            ClassEditorWindow.Open(classObj);
            return true;
        }
        

        SkillList skillListObj = EditorUtility.InstanceIDToObject(instanceID) as SkillList;
        if (skillListObj != null)
        {
            SkillEditorWindow.Open(skillListObj);
            return true;
        }


        ToolList toolListObj = EditorUtility.InstanceIDToObject(instanceID) as ToolList;
        if (toolListObj != null)
        {
            ToolEditorWindow.Open(toolListObj);
            return true;
        }


        return false;
    }
}
