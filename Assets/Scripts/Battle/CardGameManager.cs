using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class CardGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public PlayerPanel Player1Panel, Player2Panel;

    public CardsLayout Deck1, Deck2, Hand1, Hand2, Drop1, Drop2, ChooseField, ChoosedField, HandLayout;

    private CardsLayout _cardDragsFrom;
    private CardVisual _draggingCard;

    public GameObject ChoosePanel;
    public Button ApplyChoiseButton;

    private int _needToChoose = 6;
    public TextMeshProUGUI ChooseCounter; 

    private List<CardVisual> _choosedCards
    {
        get
        {
            return ChoosedField.GetComponentsInChildren<CardVisual>().ToList();
        }
    }

    private static CardGameManager instance;
  

    public static CardGameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CardGameManager>();
            return instance;
        }
    }

    public enum CardPosition
    {
        Deck = 0,
        Hand = 1,
        Drop = 2,
        Burn = 3,
        Choose = 4
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (changedProps.ContainsKey(DefaultResources.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                StartGame();
            }
        }

        if (changedProps.ContainsKey(DefaultResources.PLAYER_CHOOSED_CARDS))
        {
            if (CheckAllPlayerChoosedCards())
            {
                StartAtack();
            }
        }

        if (changedProps.ContainsKey(DefaultResources.PLAYER_DECK))
        {

        }

        if (changedProps.ContainsKey(DefaultResources.PLAYER_HAND))
        {
           
        }

        if (changedProps.ContainsKey(DefaultResources.PLAYER_DROP))
        {
           
        }
    }

    public bool CanFocus(CardVisual cardVisual)
    {
        CardsLayout layout = cardVisual.GetComponentInParent<CardsLayout>();
        if (layout == HandLayout || layout == Drop1 || layout == Drop2 || layout == Deck1 || layout == Deck2 || layout == Hand2)
        {
            return false;
        }

        return true;
    }

    private void StartAtack()
    {
        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_CHOOSED_CARDS_TO_ATACK, false}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    private bool CheckAllPlayerChoosedCards()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerChoosedCards;

            if (p.CustomProperties.TryGetValue(DefaultResources.PLAYER_CHOOSED_CARDS, out playerChoosedCards))
            {
                if ((bool)playerChoosedCards)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_draggingCard)
            {
                ReturnCard();
            }
        }
    }

    public void Start()
    {
        BattlerClass battlerClass = DefaultResources.GetClassById((int) PhotonNetwork.LocalPlayer.CustomProperties[DefaultResources.PLAYER_CLASS]);

        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_LOADED_LEVEL, true},
                {DefaultResources.PLAYER_LIVES, battlerClass.Hp}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public void CardClicked(CardVisual cardVisual)
    {
        CardsLayout cl = cardVisual.transform.parent.GetComponent<CardsLayout>();
        if (cl == ChooseField || cl == ChoosedField)
        {
            if (_choosedCards.Contains(cardVisual))
            {
                cardVisual.MoveCardTo(ChooseField);
            }
            else
            {
                if (_choosedCards.Count<_needToChoose)
                {
                    cardVisual.MoveCardTo(ChoosedField);
                }
            }

            ChooseCounter.text = "" + _choosedCards.Count+"/"+_needToChoose;

            ApplyChoiseButton.interactable = _choosedCards.Count == _needToChoose;
        }
    }

    public void MoveCardTo(CardVisual c, CardPosition position)
    {  
        CardsLayout parent = null;
        switch (position)
        {
            case CardPosition.Deck:
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    parent = Deck1;
                }
                else
                {
                    parent = Deck2;
                }
                break;
            case CardPosition.Hand:
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    parent = Hand1;
                }
                else
                {
                    parent = Hand2;
                }
                break;
            case CardPosition.Drop:
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    parent = Drop1;
                }
                else
                {
                    parent = Drop2;
                }
                break;
            case CardPosition.Choose:
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    parent = ChooseField;
                }
                else
                {
                    //parent = Drop2;
                }
                break;
        }

        c.MoveCardTo(parent, () => { });
    }

    public void DragCard(CardVisual cardVisual)
    {
        CardsLayout cl = cardVisual.transform.parent.GetComponent<CardsLayout>();

        if (cl == Hand1)
        {
            object playerChoosedCards;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(DefaultResources.PLAYER_CHOOSED_CARDS_TO_ATACK, out playerChoosedCards))
            {
                if (!(bool)playerChoosedCards)
                {
                    cardVisual.GetComponent<Image>().raycastTarget = false;
                    _cardDragsFrom = cl;
                    _draggingCard = cardVisual;
                    cardVisual.MoveCardTo(HandLayout);
                }
            }

            
        }
    }

    public void ReturnCard()
    {
        Debug.Log("return card");
        if (_draggingCard)
        {
            _draggingCard.GetComponent<Image>().raycastTarget = true;
            _draggingCard.MoveCardTo(_cardDragsFrom);
        }
        _cardDragsFrom = null;
        _draggingCard = null;
    }

    private void StartGame()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions sendOptions = new SendOptions();
        sendOptions.Reliability = true;
        PhotonNetwork.RaiseEvent(DefaultResources.START_GAME_EVENT, null, raiseEventOptions, sendOptions);

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            BattlerClass battler = DefaultResources.GetClassById((int)p.CustomProperties[DefaultResources.PLAYER_CLASS]);

            foreach (Card card in battler.Hand)
            {
                GiveCardTo(card, p, CardPosition.Hand);
            }


            foreach (Card card in battler.Deck)
            {
                GiveCardTo(card, p, CardPosition.Deck);
            }
        }

        this.photonView.RPC("StartTurn", RpcTarget.All);
    }

    [PunRPC]
    private void StartTurn()
    {
        ChoosePanel.gameObject.SetActive(true);

        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_CHOOSED_CARDS, false}
            };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        Debug.Log(Deck1.GetComponentsInChildren<CardVisual>().Count());
        foreach (CardVisual cv in Deck1.GetComponentsInChildren<CardVisual>())
        {
            Debug.Log(cv.CardAsset.CardName);
            MoveCardTo(cv, CardPosition.Choose);
        }     
    }

    private void GiveCardTo(Card card, Player p, CardPosition position)
    {
        this.photonView.RPC("GiveCardToRPC", p, DefaultResources.GetCardId(card), (int)position);
    }

    [PunRPC]
    private void GiveCardToRPC(int id, int position)
    {
        PhotonNetwork.Instantiate("Card", new Vector3(0, 0, 0), Quaternion.identity, 0, new object[] {id, position});
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(DefaultResources.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    public void OnEvent(EventData photonEvent)
    { 
            if (photonEvent.Code == DefaultResources.START_GAME_EVENT)
            {
                StartGameLocal();
            }   
    }

    private void StartGameLocal()
    {
        //init players panels
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            BattlerClass battlerClass = DefaultResources.GetClassById((int)p.CustomProperties[DefaultResources.PLAYER_CLASS]);
            if (p == PhotonNetwork.LocalPlayer)
            {
                Player1Panel.Init(p, battlerClass);
            }
            else
            {
                Player2Panel.Init(p, battlerClass);
            }
        }

        //set hp to maximum
        BattlerClass battlerClass2 = DefaultResources.GetClassById((int)PhotonNetwork.LocalPlayer.CustomProperties[DefaultResources.PLAYER_CLASS]);

        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_LIVES, battlerClass2.Hp}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public void ApplyChoise()
    {
        foreach (CardVisual cv in _choosedCards)
        {
            MoveCardTo(cv, CardPosition.Hand);
        }

        foreach (Transform t in ChooseField.transform)
        {
            MoveCardTo(t.GetComponent<CardVisual>(), CardPosition.Deck);
        }

        _needToChoose = 3;
        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_CHOOSED_CARDS, true}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        _choosedCards.Clear();
        ApplyChoiseButton.interactable = false;
        ChooseCounter.text = "0/3";
        ChoosePanel.SetActive(false);
    }

    public void SlotMouseEnter(int slotId)
    {
        Debug.Log("slot "+slotId+" enter");
    }

    public void SlotMouseUp(int slotId)
    {
        Debug.Log("slot " + slotId + " up");
    }
}
