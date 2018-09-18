using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]
public class Card : ScriptableObject
{
    public string CardName;
    public Sprite Image;
    public CardStats.Rarity Rarity;
    public int Priority;
    public string description;

    public CardStats.CardType CardType;
    public BattlerClass Battler;
}

public class CardStats
{
    public enum Rarity
    {
        Common,
        Ultimate
    }

    public enum CardType
    {
        Ancient,
        Reaction,
        Token,
        Class
    }
}
