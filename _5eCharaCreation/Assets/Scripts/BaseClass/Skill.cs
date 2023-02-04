using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum LinkedAtribute {STR = 0, DEX = 1, CON = 2, INT = 3, WIS = 4, CHA = 5}

[System.Serializable]
public class Skill
{
    public string name;
    [TextArea(6,6)]public string description;
    public LinkedAtribute linkedAtribute;
    public bool proficiency;
    public int score;

    public enum SkillPick { None, Class, Race, Origin }
    public SkillPick skillPick;

}
