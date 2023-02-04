using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Data List/Skill List", order = 0)]
public class SkillList : ScriptableObject
{
    public List<Skill> content;
}
