using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Data List/Weapon List", order = 2)]
public class WeaponList : ScriptableObject
{
    public List<Weapon> content;
}
