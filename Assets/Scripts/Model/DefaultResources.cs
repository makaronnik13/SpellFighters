using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DefaultResources
{
    //player custom properties keys
    public static string PLAYER_LOADED_LEVEL = "LevelLoaded";
    public static string PLAYER_CLASS = "PlayerClass";
    public static string PLAYER_LIVES = "PlayerLives";
    public static string PLAYER_CHOOSED_CARDS = "PlayerFinishedTurn";
    public static string PLAYER_CHOOSED_CARDS_TO_ATACK = "PlayerChoosedCardsToAtack";
    public static string PLAYER_HAND = "PlayerHand";
    public static string PLAYER_DECK = "PlayerDeck";
    public static string PLAYER_DROP = "PlayerDrop";

    //events codes
    public static byte START_GAME_EVENT = 0;


    private static Card[] _allCards = null;
    public static Card[] AllCards
    {
        get
        {
            if (_allCards == null)
            {
                _allCards = Resources.LoadAll<Card>("Cards");
            }
            return _allCards;
        }
    }

    private static BattlerClass[] _allClasses = null;
    public static BattlerClass[] AllClasses
    {
        get
        {
            if (_allClasses == null)
            {
                _allClasses = Resources.LoadAll<BattlerClass>("Classes");
            }
            return _allClasses;
        }
    }
    public static BattlerClass GetClassById(int id)
    {
        return AllClasses[id];
    }

    public static int GetCardId(Card card)
    {
        return AllCards.ToList().IndexOf(card);
    }

    public static Card GetCardById(int i)
    {
        return AllCards[i];
    }

    public static Color GetClassColor(CardStats.CardType cardType, BattlerClass battler)
    {
        switch (cardType)
        {
            case CardStats.CardType.Ancient:
                return new Color(0.637f, 0.637f, 0.637f);
            case CardStats.CardType.Reaction:
                return new Color(132f / 256f, 141f / 256f, 109f / 256f);
            case CardStats.CardType.Token:
                return new Color(109f / 256f, 134f / 256f, 141f / 256f);
            case CardStats.CardType.Class:
                return battler.BattlerColor;
        }

        return Color.white;
    }
}