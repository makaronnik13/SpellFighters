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
    private CardsLayout _focusedSlot;
    private CardsLayout FocusedSlot
    {
        get
        {
            return _focusedSlot;
        }
        set
        {
            _focusedSlot = value;
        }
    }

    public CardsLayout[] MyPlaySlots = new CardsLayout[3];
    public CardsLayout[] EnemyPlaySlots = new CardsLayout[3];

    public GameObject ChoosePanel;
    public Button ApplyChoiseButton;
    public Button EndAttackButton;

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

        if (changedProps.ContainsKey(DefaultResources.PLAYER_TURN))
        {
            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.SelectCards))
            {
                photonView.RPC("MoveDropToPile", RpcTarget.All);

                for (int i = 0; i<3; i++)
                {
                    photonView.RPC("ClearCardSlot", RpcTarget.All, new object[] { i });
                }

                photonView.RPC("StartTurn", RpcTarget.All);
            }

            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.SelectCardsToAtack))
            {
                Debug.Log("StartAtack");
                //StartAtack();
            }

            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.PlayingCard1))
            {
                PlayCards(0);
                //this.photonView.RPC("PlayCard", RpcTarget.All);
            }

            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.PlayingCard2))
            {
                PlayCards(1);
                //this.photonView.RPC("PlayCard", RpcTarget.All);
            }

            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.PlayingCard3))
            {
                PlayCards(2);
                //this.photonView.RPC("PlayCard", RpcTarget.All);
            }

            if (CheckAllPlayerOnTurn(DefaultResources.GameTurn.Reaction1) || CheckAllPlayerOnTurn(DefaultResources.GameTurn.Reaction2))
            {
                React();
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

    private void React()
    {
        photonView.RPC("ReactOnClient", RpcTarget.All);
        Debug.Log("reaction turn");
    }

    [PunRPC]
    private void ReactOnClient()
    {
        EndAttackButton.gameObject.SetActive(true);
        EndAttackButton.interactable = true;
        UpdateCardPlayableState();
    }

    private void PlayCards(int slot)
    {
        Debug.Log("play cards in "+slot+" slot");

        photonView.RPC("OpenCardsInSlot", RpcTarget.All, new object[] { slot });

        PlayCards(MyPlaySlots[slot].GetComponentInChildren<CardVisual>().CardAsset, EnemyPlaySlots[slot].GetComponentInChildren<CardVisual>().CardAsset, ()=>
        {
            photonView.RPC("ContinueTurn", RpcTarget.All, new object[] { slot });
        });
    }

    private void PlayCards(Card cardAsset1, Card cardAsset2, Action callback = null)
    {
        Debug.Log("play cards on server: " + cardAsset1.CardName +" and "+cardAsset2);
        
   

        if (callback!=null)
        {
            StartCoroutine(InvokeAferTime(5, callback));
        }
    }

    private IEnumerator InvokeAferTime(int waitTime, Action callback)
    {
        yield return new WaitForSeconds(waitTime);
        callback();
    }

    [PunRPC]
    private void OpenCardsInSlot(int i)
    {
        EnemyPlaySlots[i].GetComponentInChildren<CardVisual>().Visible = true;
    }

    [PunRPC]
    private void MoveDropToPile()
    {
        foreach (CardVisual cv in Drop1.GetComponentsInChildren<CardVisual>())
        {
            MoveCardTo(cv, Deck1, true);
        }
    }

    [PunRPC]
    private void ContinueTurn(int i)
    {
        int nextTurnId = i * 2 + 3;
        if (i == 2) //if this is the last card
        {
            nextTurnId = 0;
        }

        Debug.Log("Next turn id: "+nextTurnId);
        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_TURN, nextTurnId}, //next reaction turn    
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    [PunRPC]
    private void ClearCardSlot(int i)
    {
        CardVisual cv = MyPlaySlots[i].GetComponentInChildren<CardVisual>();
        if (cv)
        {
            if (cv.CardAsset.Rarity == CardStats.Rarity.Ultimate)
            {
                cv.Burn();
            }
            else
            {
                MoveCardTo(cv, Drop1, true);
            }
        }
    }

    private bool CheckAllPlayerOnTurn(DefaultResources.GameTurn turn)
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (GetPlayersTurn(p) == turn)
            {
                continue;
            }
            return false;
        }

        return true;
    }

    private DefaultResources.GameTurn GetPlayersTurn(Player player)
    {
        object playerTurn;

        if (player.CustomProperties.TryGetValue(DefaultResources.PLAYER_TURN, out playerTurn))
        {
            return (DefaultResources.GameTurn) (int)playerTurn;
        }

        return DefaultResources.GameTurn.Error;
    }

    public void CardFocused(CardVisual card)
    {      
        if (MyPlaySlots.Contains(card.transform.parent.GetComponent<CardsLayout>()) || EnemyPlaySlots.Contains(card.transform.parent.GetComponent<CardsLayout>()))
        {          
            card.transform.parent.SetAsLastSibling();
            card.transform.parent.parent.SetAsLastSibling();
        }
    }

    public bool CanFocus(CardVisual cardVisual)
    {
        CardsLayout layout = cardVisual.GetComponentInParent<CardsLayout>();
        if (layout == HandLayout || layout == Drop1 || layout == Drop2 || layout == Deck1 || layout == Deck2 || layout == Hand2)
        {
            return false;
        }

        if (_draggingCard)
        {
            return false;
        }

        if (cardVisual.Visible)
        {
            return false;
        }
        return true;
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
        if (cl == ChooseField)
        {
            if (_choosedCards.Contains(cardVisual))
            {
                MoveCardTo(cardVisual, ChoosedField);
            }
            else
            {
                if (_choosedCards.Count<_needToChoose)
                {
                    MoveCardTo(cardVisual, ChoosedField);
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
        }
        
        MoveCardTo(c, parent);     
    }

    public void DragCard(CardVisual cardVisual)
    {
        CardsLayout cl = cardVisual.transform.parent.GetComponent<CardsLayout>();

        if (cl == Hand1 || MyPlaySlots.Contains(cl))
        {
     
            if (GetPlayersTurn(PhotonNetwork.LocalPlayer) == DefaultResources.GameTurn.SelectCardsToAtack)
            {
               
                    cardVisual.GetComponent<Image>().raycastTarget = false;

                    _cardDragsFrom = cl;

                    _draggingCard = cardVisual;
                    MoveCardTo(cardVisual, HandLayout);      
            }           
        }
    }

    public void ReturnCard()
    {
        if (_draggingCard)
        {
            _draggingCard.GetComponent<Image>().raycastTarget = true;
            MoveCardTo(_draggingCard, _cardDragsFrom, true);
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

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_TURN, (int)DefaultResources.GameTurn.SelectCards}
            };

            p.SetCustomProperties(props);
        }
        
    }

    [PunRPC]
    private void StartTurn()
    {
        ChoosePanel.gameObject.SetActive(true);

        foreach (CardVisual cv in Deck1.GetComponentsInChildren<CardVisual>())
        {
            MoveCardTo(cv, ChooseField);
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
            MoveCardTo(cv, Hand1, true);
        }

        foreach (Transform t in ChooseField.transform)
        {
            MoveCardTo(t.GetComponent<CardVisual>(), Deck1);
        }

        _needToChoose = 3;
        Hashtable props = new Hashtable
            {
                {DefaultResources.PLAYER_TURN, (int)DefaultResources.GameTurn.SelectCardsToAtack}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        _choosedCards.Clear();
        ApplyChoiseButton.interactable = false;
        ChooseCounter.text = "0/3";
        ChoosePanel.SetActive(false);
        EndAttackButton.gameObject.SetActive(true);

        UpdateCardPlayableState();
    }

    private void UpdateCardPlayableState()
    {
        foreach (CardVisual cv in Hand1.GetComponentsInChildren<CardVisual>())
        {
            cv.CanBePlayed = true;

            if (cv.CardAsset.CardType == CardStats.CardType.Reaction && GetPlayersTurn(PhotonNetwork.LocalPlayer) == DefaultResources.GameTurn.SelectCardsToAtack)
            {
                cv.CanBePlayed = false;
            }

            if (cv.CardAsset.CardType != CardStats.CardType.Reaction && (GetPlayersTurn(PhotonNetwork.LocalPlayer) == DefaultResources.GameTurn.Reaction1 || GetPlayersTurn(PhotonNetwork.LocalPlayer) == DefaultResources.GameTurn.Reaction2))
            {
                cv.CanBePlayed = false;
            }
        }
    }

    public void EndAttack()
    {
        EndAttackButton.gameObject.SetActive(false);
        Hashtable props = new Hashtable();

        switch (GetPlayersTurn(PhotonNetwork.LocalPlayer))
        {
            case DefaultResources.GameTurn.SelectCardsToAtack:
                props.Add(DefaultResources.PLAYER_TURN, (int)DefaultResources.GameTurn.PlayingCard1);
                break;
            case DefaultResources.GameTurn.Reaction1:
                props.Add(DefaultResources.PLAYER_TURN, (int)DefaultResources.GameTurn.PlayingCard2);
                break;
            case DefaultResources.GameTurn.Reaction2:
                props.Add(DefaultResources.PLAYER_TURN, (int)DefaultResources.GameTurn.PlayingCard3);
                break;
        }
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public void SlotMouseEnter(int slotId)
    {
        if (_draggingCard)
        {
            FocusedSlot = MyPlaySlots[slotId];
        }
    }

    public void SlotMouseExit(int slotId)
    {
        if (FocusedSlot == MyPlaySlots[slotId])
        {
            FocusedSlot = null;
        }
    }

    public void DragingCardEnded()
    {
        bool canPlace = FocusedSlot && _draggingCard;

        canPlace = canPlace && _draggingCard.CardAsset.CardType != CardStats.CardType.Reaction;

        if (MyPlaySlots.Contains(FocusedSlot))
        {
            canPlace = canPlace && !(_draggingCard.CardAsset.CardType == CardStats.CardType.Token && MyPlaySlots.ToList().IndexOf(FocusedSlot) == 0);
        }
        

        if (canPlace)
        {
            CardVisual cardInSlot = FocusedSlot.GetComponentInChildren<CardVisual>();

            MoveCardTo(_draggingCard, FocusedSlot, true);
            _draggingCard.GetComponent<Image>().raycastTarget = true;
            _draggingCard = null;
          
            if (cardInSlot)
            {
                if (MyPlaySlots.Contains(_cardDragsFrom))
                {
                    MoveCardTo(cardInSlot, _cardDragsFrom, true);
                }
                else
                {
                    _draggingCard = cardInSlot;
                    ReturnCard();
                }
            }
        }
        else
        {
            if (MyPlaySlots.Contains(_cardDragsFrom))
            {
                _cardDragsFrom = Hand1;
            }
            ReturnCard();
        }

        bool canEndAtack = true;
        foreach (CardsLayout cl in MyPlaySlots)
        {
            if (cl.transform.childCount == 0)
            {
                canEndAtack = false;
            }
        }

        EndAttackButton.interactable = canEndAtack; 
    }

    private void MoveCardTo(CardVisual card, CardsLayout layout, bool sync = false)
    {
        if (sync)
        {
            int layoutId = GetLayoutId(layout);       
            if (MyPlaySlots.Contains(layout))
            {
                int slot = MyPlaySlots.ToList().IndexOf(layout);
                layoutId = GetLayoutId(EnemyPlaySlots[slot]);
            }
            if (layout == Deck1)
            {
                layoutId = GetLayoutId(Deck2);
            }
            if (layout == Drop1)
            {
                layoutId = GetLayoutId(Drop2);
            }
            if (layout == Hand1)
            {
                layoutId = GetLayoutId(Hand2);
            }

            photonView.RPC("MoveCardOnClient", PhotonNetwork.LocalPlayer, new object[] { card.photonView.ViewID, GetLayoutId(layout)});
            photonView.RPC("MoveCardOnClient", RpcTarget.Others, new object[] { card.photonView.ViewID, layoutId });
        }
        else
        {
            card.MoveCardTo(layout);
        }
    }

    [PunRPC]
    private void MoveCardOnClient(int cardViewId, int layoutId)
    {
        PhotonView cardView = PhotonView.Find(cardViewId);
        CardsLayout newLayout = GetLayoutById(layoutId);
        if (newLayout)
        {
            cardView.GetComponent<CardVisual>().MoveCardTo(newLayout);
        }
    }

    private CardsLayout GetLayoutById(int layoutId)
    {
        Dictionary<int, CardsLayout> ids = new Dictionary<int, CardsLayout>
        {
            {0, Deck1},
            {1, Deck2},
            {2, Hand1},
            {3, Hand2},
            {4, Drop1},
            {5, Drop2},
            {6, MyPlaySlots[0]},
            {7, MyPlaySlots[1]},
            {8, MyPlaySlots[2]},
            {9, EnemyPlaySlots[0]},
            {10, EnemyPlaySlots[1]},
            {11, EnemyPlaySlots[2]}
        };

        if (ids.ContainsKey(layoutId))
        {
            return ids[layoutId];
        }

        return null;
    }

    private int GetLayoutId(CardsLayout layout)
    {
        Dictionary<CardsLayout, int> ids = new Dictionary<CardsLayout, int>
        {
            { Deck1,0 },
            { Deck2,1 },
            { Hand1,2 },
            { Hand2,3 },
            { Drop1,4 },
            { Drop2,5 },
            { MyPlaySlots[0],6 },
            { MyPlaySlots[1],7 },
            { MyPlaySlots[2],8 },
            { EnemyPlaySlots[0],9 },
            { EnemyPlaySlots[1],10 },
            { EnemyPlaySlots[2],11 }
        };

        if (ids.ContainsKey(layout))
        {
            return ids[layout];
        }

        return -1;
    }
}
