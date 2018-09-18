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

namespace Photon.Pun.Demo.Asteroids
{
    public class PlayerListEntry : MonoBehaviour
    {
        [Header("UI References")]
        public TMPro.TextMeshProUGUI PlayerNameText;
        public TMPro.TextMeshProUGUI PlayerClassText;

        public Image PlayerClassImage;
       

        private int ownerId;
      

        #region UNITY

        public void OnEnable()
        {
            PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
        }

        public void Start()
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == ownerId)
            {
                    Hashtable props = new Hashtable() {{AsteroidsGame.PLAYER_READY, true}};
                    PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                    if (PhotonNetwork.IsMasterClient)
                    {
                       // FindObjectOfType<LobbyMainPanel>().LocalPlayerPropertiesUpdated();
                    }
 
            }
        }

        public void OnDisable()
        {
            PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
        }

        #endregion

        public void Initialize(int playerId, string playerName)
        {
            ownerId = playerId;
            PlayerNameText.text = playerName;           
            BattlerClass bClass = DefaultResources.GetClassById((int)PhotonNetwork.LocalPlayer.CustomProperties[DefaultResources.PLAYER_CLASS]);
            PlayerClassImage.sprite = bClass.BattlerImage;
            PlayerClassText.text = bClass.BattlerName;

    }

        private void OnPlayerNumberingChanged()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                if (p.ActorNumber == ownerId)
                {
                    PlayerClassImage.color = AsteroidsGame.GetColor(p.GetPlayerNumber());
                }
            }
        }

    }
}