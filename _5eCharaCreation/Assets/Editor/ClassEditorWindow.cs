using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClassEditorWindow : EditorWindow
{
    Class target;
    List<Skill> skillData = new List<Skill>();
    List<bool> skillToggles = new List<bool>();

    bool[] armor = new bool[4];
    bool propretyFoldout;
    bool skillFoldout;

    Vector2 scrollPos;

    public static void Open(Class _class)
    {
        ClassEditorWindow window = GetWindow<ClassEditorWindow>("Class Editor");
        window.target = _class;
    }

    private void OnGUI()
    {
        Init();
        DrawHeader();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
        EditorGUI.indentLevel += 1;
        EditorGUILayout.BeginHorizontal("box");
        target.sprite = (Sprite)EditorGUILayout.ObjectField(target.sprite, typeof(Sprite), false, GUILayout.Width(256), GUILayout.Height(320));
        EditorGUILayout.BeginVertical(GUILayout.Width(728));
        DrawTextSection();
        DrawAbilitiesSection();
        DrawArmorSection();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical("box", GUILayout.Width(993));
        DrawPropertiesSection();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical("box", GUILayout.Width(993));
        DrawSkillSection();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(2);

        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndScrollView();
    }

    #region Methods

    void Init()
    {
        if (skillData.Count == 0)
        {
            SkillList asset = (SkillList)AssetDatabase.LoadAssetAtPath("Assets/Ressources/DataList/Skill List.asset", typeof(SkillList));
            skillData = asset.content;
        }

    }

    void DrawHeader()
    {
        EditorStyles.textArea.wordWrap = true;
        EditorStyles.boldLabel.fontSize = 20;
        EditorStyles.boldLabel.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(target.name, EditorStyles.boldLabel, GUILayout.Height(30), GUILayout.Width(1000));
        EditorStyles.boldLabel.fontSize = 12;
        EditorStyles.boldLabel.alignment = TextAnchor.MiddleLeft;
    }

    void DrawTextSection()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(720));
        EditorGUI.indentLevel += 1;
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Alternate Name");

        EditorGUILayout.BeginHorizontal(GUILayout.Width(700));
        target.altName = EditorGUILayout.TextField(target.altName,GUILayout.Width(370));
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Base HP", GUILayout.Width(80));
        target.baseHP = EditorGUILayout.IntField(target.baseHP, GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Description");
        
        target.description = EditorGUILayout.TextArea(target.description, EditorStyles.textArea ,GUILayout.Width(700), GUILayout.Height(120));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndVertical();
    }

    void DrawAbilitiesSection()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(600));
        EditorGUILayout.Space(50);
        EditorGUILayout.Space(50);

        EditorGUILayout.BeginVertical();
        for (int i = 0; i < 3; i++)
            DrawSingleAbility(i);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(60);
        EditorGUILayout.BeginVertical();
        for (int i = 3; i < 6; i++)
            DrawSingleAbility(i);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }

    void DrawArmorSection()
    {
        EditorGUILayout.Space(35);

        for (int i = 1; i < 1 + target.armorTier; i++)
            armor[i] = true;
        armor[0] = target.shieldEquip;

        GUILayout.BeginHorizontal(GUILayout.Width(700));
        EditorGUILayout.Space(20);

        EditorGUI.BeginChangeCheck();
        for (int i = 1; i < 4; i++)
            armor[i] = GUILayout.Toggle(armor[i], GetArmorType(i) + " Armor");

        EditorGUILayout.Space(5);

        armor[0] = GUILayout.Toggle(armor[0], "Shield");
        GUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
            target.shieldEquip = armor[0];
            target.armorTier = (armor[1] ? 1 : 0) + (armor[2] ? 1 : 0) + (armor[3] ? 1 : 0);
        }
    }

    void DrawPropertiesSection()
    {
        propretyFoldout = EditorGUILayout.Foldout(propretyFoldout, "Class Properties");
        if (propretyFoldout)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.BeginHorizontal(GUILayout.Width(964));
            for (int i = 0; i < target.ClassProperty.Length; i++)
                DrawSingleProperty(i);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel -= 1;
        }
    }

    void DrawSkillSection()
    {


        skillFoldout = EditorGUILayout.Foldout(skillFoldout, "Class Skills");
        if (skillFoldout)
        {
            EditorGUILayout.Space(10);
            EditorGUI.indentLevel += 1;
            if (skillToggles.Count != skillData.Count)
            {
                skillToggles.Clear();
                skillToggles.AddRange(new bool[skillData.Count]);
            }
            LoadClassSkills();
            EditorGUI.BeginChangeCheck();
            EditorGUI.indentLevel += 1;
            
            EditorGUILayout.BeginHorizontal(GUILayout.Width(964));
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < skillToggles.Count; i++)
            {
                skillToggles[i] = GUILayout.Toggle(skillToggles[i], skillData[i].name, GUILayout.Width(140));
                EditorGUILayout.Space(2);
                if ((i + 1) % 6 == 0)
                {
                    EditorGUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.BeginVertical();
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel -= 1;

            if (EditorGUI.EndChangeCheck())
            {
                WriteClassSkills();
            }
            EditorGUI.indentLevel -= 1;
        }
    }

    void DrawSingleAbility(int i)
    {
        
        AbilityData ability = target.classAbilities.GetFromIndex(i);
        EditorGUILayout.BeginHorizontal();

        ability.ST_proficiency = GUILayout.Toggle(ability.ST_proficiency, "");
        EditorGUILayout.LabelField(GetAbilityTag(i), GUILayout.Width(70));
        ability.weight = EditorGUILayout.FloatField(ability.weight, GUILayout.Width(45));
        EditorGUILayout.LabelField("Weight", GUILayout.Width(80));

        EditorGUILayout.EndHorizontal();
    }

    void DrawSingleProperty(int i)
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Name");
        target.ClassProperty[i].name = EditorGUILayout.TextField(target.ClassProperty[i].name, GUILayout.Width(200));
        EditorGUILayout.LabelField("Description");
        target.ClassProperty[i].description = EditorGUILayout.TextArea(target.ClassProperty[i].description, EditorStyles.textArea, GUILayout.Height(120), GUILayout.Width(313));
        EditorGUILayout.EndVertical();

    }

    string GetAbilityTag(int i)
    {
        switch (i)
        {
            case 0: return "STR";
            case 1: return "DEX";
            case 2: return "CON";
            case 3: return "INT";
            case 4: return "WIS";
            case 5: return "CHA";
            default: throw new System.NotImplementedException();
        }
    }

    string GetArmorType(int i)
    {
        switch (i)
        {
            case 1: return "Light";
            case 2: return "Medium";
            case 3: return "Heavy";
            default: throw new System.NotImplementedException();
        }
    }

    void LoadClassSkills()
    {
        for (int i = 0; i < skillData.Count; i++)
            skillToggles[i] = target.classSkills.Contains(skillData[i]);
    }

    void WriteClassSkills()
    {
        List<Skill> tempList = new List<Skill>();
        for (int i = 0; i < skillToggles.Count; i++)
            if (skillToggles[i])
                tempList.Add(skillData[i]);
        target.classSkills = tempList;
    }

    #endregion


}
