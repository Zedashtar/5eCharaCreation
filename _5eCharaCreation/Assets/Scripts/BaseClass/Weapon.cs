using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System;

[Serializable]
public enum Dice
{
    [Description("1")] _1,
    [Description("1d4")] _1d4,
    [Description("1d6")] _1d6,
    [Description("1d8")] _1d8,
    [Description("1d10")] _1d10,
    [Description("1d12")] _1d12,
    [Description("2d6")] _2d6
}

[Serializable]
public enum DamageType
{
    Tranchant,
    Perforant,
    Contondant
}

[Serializable]
public class Weapon 
{
    public string name;
    public Dice damageDice;
    public DamageType damageType;
    public int range;

    public enum Type { Simple, War}
    public Type type;
    public Property properties;


    [Flags]
    [Serializable]
    public enum Property
    {
        None = 0,
        [Description("Deux Mains")] Deux_Mains = 1,
        Allonge = 2,
        Chargement = 4,
        Finesse = 8,
        Lancer = 16,
        Légère = 32,
        Lourde = 64,
        Munitions = 128,
        Polyvalente = 256,
        Portée = 512,
        All = ~0
    }




}
