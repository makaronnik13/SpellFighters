using UnityEngine;

[CreateAssetMenu(fileName = "Battler", menuName = "Battler", order = 2)]
public class BattlerClass : ScriptableObject
{
    public string BattlerName;
    public Sprite BattlerImage;
    public int Hp;
    public string Description;
    public Card[] Hand;
    public Card[] Deck;
    public Color BattlerColor;
}