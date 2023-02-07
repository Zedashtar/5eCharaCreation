using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ClassEditorWindow : EditorWindow
{
    Class target;
    List<Skill> skillData = new List<Skill>();
    List<bool> skillToggles = new List<bool>();

    List<Weapon> weaponData = new List<Weapon>();
    List<Weapon> simpleWeapons = new List<Weapon>();
    List<Weapon> warWeapons = new List<Weapon>();
    List<bool> simpleWeaponsToggles = new List<bool>();
    List<bool> warWeaponsToggles = new List<bool>();

    bool[] armor = new bool[4];
    bool propretyFoldout = true;
    bool skillFoldout = true;
    bool weaponFoldout = true;

    bool b_thievesTool;
    Tool thievesTool;
    bool b_tinkersTool;
    Tool tinkersTool;
    bool b_herbalistKit;
    Tool herbalistKit;

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
        EditorGUILayout.BeginVertical("box", GUILayout.Width(993));
        DrawWeaponSection();
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
            SkillList skillAsset = (SkillList)AssetDatabase.LoadAssetAtPath("Assets/Ressources/DataList/Skill List.asset", typeof(SkillList));
            skillData = skillAsset.content;
        }

        if (skillToggles.Count != skillData.Count)
        {
            skillToggles.Clear();
            skillToggles.AddRange(new bool[skillData.Count]);
        }

        ToolList toolAsset = (ToolList)AssetDatabase.LoadAssetAtPath("Assets/Ressources/DataList/Tool List.asset", typeof(ToolList));
        List<Tool> tempList = toolAsset.content;
        foreach (Tool tool in tempList)
        {
            if (tool.name == "Outils de voleur")
                thievesTool = tool;
            if (tool.name == "Outils de bricoleur")
                tinkersTool = tool;
            if (tool.name == "Matériel d'herboriste")
                herbalistKit = tool;
        }

        if (weaponData.Count == 0)
        {
            WeaponList weaponAsset = (WeaponList)AssetDatabase.LoadAssetAtPath("Assets/Ressources/DataList/Weapon List.asset", typeof(WeaponList));
            weaponData = weaponAsset.content;
        }

        if (simpleWeapons.Count == 0)
        {
            foreach (Weapon weapon in weaponData)
            {
                if (weapon.type == Weapon.Type.Simple)
                    simpleWeapons.Add(weapon);
            }
        }

        if (warWeapons.Count == 0)
        {
            foreach (Weapon weapon in weaponData)
            {
                if (weapon.type == Weapon.Type.War)
                    warWeapons.Add(weapon);
            }
        }

        if (simpleWeaponsToggles.Count != simpleWeapons.Count)
        {
            simpleWeaponsToggles.Clear();
            simpleWeaponsToggles.AddRange(new bool[simpleWeapons.Count]);
        }
        if (warWeaponsToggles.Count != warWeapons.Count)
        {
            warWeaponsToggles.Clear();
            warWeaponsToggles.AddRange(new bool[warWeapons.Count]);
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

            LoadClassSkills();
            LoadClassTools();
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

            EditorGUILayout.Space(40);

            EditorGUILayout.BeginHorizontal(GUILayout.Width(964));
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            b_thievesTool = GUILayout.Toggle(b_thievesTool, "Thieves' Tool", GUILayout.Width(140));
            EditorGUILayout.Space(2);
            b_tinkersTool = GUILayout.Toggle(b_tinkersTool, "Tinker's Tool", GUILayout.Width(140));
            EditorGUILayout.Space(2);
            b_herbalistKit = GUILayout.Toggle(b_herbalistKit, "Herbalist Kit", GUILayout.Width(140));
            EditorGUILayout.Space(2);
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            target.artisanSkillToken = EditorGUILayout.IntField("Artisan Skill Tokens", target.artisanSkillToken);
            target.musicSkillToken = EditorGUILayout.IntField("Music Skill Tokens", target.musicSkillToken);
            target.mixedSkillToken = EditorGUILayout.IntField("Mixed Skill Tokens", target.mixedSkillToken);
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();


            if (EditorGUI.EndChangeCheck())
            {
                WriteClassSkills();
                WriteClassTools();
            }
            EditorGUI.indentLevel -= 1;
        }
    }

    private void DrawWeaponSection()
    {
        weaponFoldout = EditorGUILayout.Foldout(propretyFoldout, "Class Weapons");
        if (weaponFoldout)
        {
            EditorGUI.indentLevel += 1;
            LoadClassWeapons();
            EditorGUILayout.Space(20);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal(GUILayout.Width(964));

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("All", EditorStyles.miniButton))
                for (int i = 0; i < simpleWeaponsToggles.Count; i++)
                    simpleWeaponsToggles[i] = true;
            if (GUILayout.Button("None", EditorStyles.miniButton))
                for (int i = 0; i < simpleWeaponsToggles.Count; i++)
                    simpleWeaponsToggles[i] = false;
            EditorGUILayout.LabelField("Simple Weapons", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("All", EditorStyles.miniButton))
                for (int i = 0; i < warWeaponsToggles.Count; i++)
                    warWeaponsToggles[i] = true;
            if (GUILayout.Button("None", EditorStyles.miniButton))
                for (int i = 0; i < warWeaponsToggles.Count; i++)
                    warWeaponsToggles[i] = false;
            EditorGUILayout.LabelField("War Weapons", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal(GUILayout.Width(964));
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < simpleWeaponsToggles.Count; i++)
            {
                simpleWeaponsToggles[i] = GUILayout.Toggle(simpleWeaponsToggles[i], simpleWeapons[i].name, GUILayout.Width(140));
                EditorGUILayout.Space(2);
            }
            EditorGUILayout.EndVertical();


 
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < warWeaponsToggles.Count; i++)
            {
                warWeaponsToggles[i] = GUILayout.Toggle(warWeaponsToggles[i], warWeapons[i].name, GUILayout.Width(140));
                EditorGUILayout.Space(2);
            }
            EditorGUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                WriteClassWeapons();
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

    void LoadClassTools()
    {
        b_thievesTool = false;
        b_tinkersTool = false;
        b_herbalistKit = false;
        if(target.classTools.Count != 0)
        {
            foreach (Tool tool in target.classTools)
            {
                if (tool.name == "Outils de voleur") { }
                    b_thievesTool = true;
                if (tool.name == "Outils de bricoleur")
                    b_tinkersTool = true;
                if (tool.name == "Matériel d'herboriste")
                    b_herbalistKit = true;
            }
        }
    }

    void WriteClassTools()
    {
        List<Tool> tempList = new List<Tool>();

        if (b_thievesTool)
            tempList.Add(thievesTool);
        if (b_tinkersTool)
            tempList.Add(tinkersTool);
        if (b_herbalistKit)
            tempList.Add(herbalistKit);

        target.classTools = tempList;

    }

    void LoadClassWeapons()
    {
        for (int i = 0; i < simpleWeapons.Count; i++)
            simpleWeaponsToggles[i] = target.classWeapons.Contains(simpleWeapons[i]);
        for (int i = 0; i < warWeapons.Count; i++)
            warWeaponsToggles[i] = target.classWeapons.Contains(warWeapons[i]);
    }

    void WriteClassWeapons()
    {
        List<Weapon> tempList = new List<Weapon>();
        for (int i = 0; i < simpleWeaponsToggles.Count; i++)
            if (simpleWeaponsToggles[i])
                tempList.Add(simpleWeapons[i]);

        for (int i = 0; i < warWeaponsToggles.Count; i++)
            if (warWeaponsToggles[i])
                tempList.Add(warWeapons[i]);

        target.classWeapons = tempList;
    }
    #endregion


}
