// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerListEntry.cs" company="Exit Games GmbH">
//   Part of: Asteroid Demo,
// </copyright>
// <summary>
//  Player List Entry
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerListEntry : MonoBehaviour
    {
        [Header("UI References")]
        public TMPro.TextMeshProUGUI PlayerNameText;
        public TMPro.TextMeshProUGUI PlayerClassText;

        public Image PlayerClassImage;
       

        private Player owner;
      

        #region UNITY

        

        public void Start()
        {
            if (PhotonNetwork.LocalPlayer == owner)
            {   
                    if (PhotonNetwork.IsMasterClient)
                    {
                       // FindObjectOfType<LobbyMainPanel>().LocalPlayerPropertiesUpdated();
                    }
            }
        }

        #endregion

        public void Initialize(Player player)
        {
            owner = player;
            PlayerNameText.text = player.NickName;
            BattlerClass bClass = DefaultResources.GetClassById((int)player.CustomProperties[DefaultResources.PLAYER_CLASS]);
            PlayerClassImage.sprite = bClass.BattlerImage;
            PlayerClassText.text = bClass.BattlerName;

    }

       

}