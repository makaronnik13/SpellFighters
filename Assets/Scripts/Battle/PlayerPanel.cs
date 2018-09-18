using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPanel : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI HpCounter, PlayerName;
    public Image ClassImage;

    private Player _owner;

    public void Init(Player player, BattlerClass battler)
    {
        _owner = player;
        HpCounter.text = battler.Hp + "";
        PlayerName.text = player.NickName;
        ClassImage.sprite = battler.BattlerImage;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(DefaultResources.PLAYER_LIVES) && _owner == targetPlayer)
        {
            HpCounter.text = "" + targetPlayer.CustomProperties[DefaultResources.PLAYER_LIVES];
        }   
    }
}
