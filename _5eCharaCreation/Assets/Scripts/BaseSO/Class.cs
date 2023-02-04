using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "", menuName = "Data/Class", order = 0)]
public class Class : ScriptableObject
{
    public Sprite sprite;
    public string altName;
    public string description;
    public int baseHP;
    public Abilities classAbilities = new Abilities();
    [Range(0, 3)] public int armorTier;
    public bool shieldEquip;

    PropertyField[] classProperty = new PropertyField[3];
    public PropertyField[] ClassProperty
    {
        get { return classProperty; }
        set { classProperty = value; }
    }

    public List<Skill> classSkills = new List<Skill>();

    
    
}
