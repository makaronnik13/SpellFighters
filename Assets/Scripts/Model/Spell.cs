using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 1)]
public class Spell : ScriptableObject
{
    public int Priority;
    public  SpellAim Aim;
    public int Value;
    public SpellType Type;

    public enum SpellAim
    {
        None,
        You,
        Enemy,
        Choose
    }

    public enum SpellType
    {
        Damage,
        Heal
    }
}
