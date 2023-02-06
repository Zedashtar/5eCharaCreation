using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool 
{
    public string name;
    [TextArea(6, 6)] public string description;
    public bool proficiency;

    public enum Type { None, Musical, Artisanal, Game }
    public Type type;
}
