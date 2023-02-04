using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData
    
{
    public int value;
    public int mod;
    public string s_mod => mod < 0 ? "" : "+" + mod.ToString();
    public float weight;
    public bool ST_proficiency;
}

public class Abilities : IEnumerable
{
    public AbilityData strength;
    public AbilityData dexterity;
    public AbilityData constitution;
    public AbilityData intelligence;
    public AbilityData wisdom;
    public AbilityData charisma;

    public Abilities()
    {
        strength = new AbilityData();
        dexterity = new AbilityData();
        constitution = new AbilityData();
        intelligence = new AbilityData();
        wisdom = new AbilityData();
        charisma = new AbilityData();
    }


    #region IEnumerator
    public IEnumerator GetEnumerator()
    {
        return new AbilitieScoresEnumerator(this);
    }

    public class AbilitieScoresEnumerator : IEnumerator
    {
        int _index = 1;
        Abilities ab;
        public AbilitieScoresEnumerator(Abilities _ab){ab = _ab;}
        object IEnumerator.Current
        {
            get
            {
                switch (_index)
                {
                    case 0: return ab.strength;
                    case 1: return ab.dexterity;
                    case 2: return ab.constitution;
                    case 3: return ab.intelligence;
                    case 4: return ab.wisdom;
                    case 5: return ab.charisma;
                    default: throw new System.NotImplementedException();
                }
            }
        }

        bool IEnumerator.MoveNext()
        {
            _index++;
            return _index < 6;
        }

        void IEnumerator.Reset()
        {
            _index = -1;
        }
    }

    public AbilityData GetFromIndex(int i)
    {
        switch (i)
        {
            case 0: return strength;
            case 1: return dexterity;
            case 2: return constitution;
            case 3: return intelligence;
            case 4: return wisdom;
            case 5: return charisma;
            default: throw new System.NotImplementedException();
        }
    }
    #endregion
}
