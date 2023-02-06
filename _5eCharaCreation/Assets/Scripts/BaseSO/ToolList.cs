using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Data List/Tool List", order = 1)]
public class ToolList : ScriptableObject
{
    public List<Tool> content;
}
