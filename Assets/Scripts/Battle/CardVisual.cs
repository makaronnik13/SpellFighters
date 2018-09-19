using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using Photon.Pun;

public class CardVisual : MonoBehaviourPunCallbacks, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPunInstantiateMagicCallback, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private int _lastSibling;

    private static float _movementSpeed = 0.3f;
    private static float _scaleSpeed = 0.2f;
    private bool _hovered = false;

    public GameObject Back;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Priority;
    public Image ClassPanel;
    public Image Rarity;
    public Image Picture;
    public TextMeshProUGUI Name;
    private Transform _oldParentTransform;
    private Card _card;
    public Card CardAsset
    {
        get
        {
            return _card;
        }
    }


    public void Init(Card card, CardGameManager.CardPosition position, bool show)
    {
        _card = card;
        Name.text = card.CardName;
        Picture.sprite = card.Image;
        Description.text = card.description;
        Priority.text = "" + card.Priority;
        ClassPanel.color = DefaultResources.GetClassColor(card.CardType, card.Battler);
        Priority.transform.parent.GetComponent<Image>().color = DefaultResources.GetClassColor(card.CardType, card.Battler);
        Rarity.enabled = (card.Rarity == CardStats.Rarity.Ultimate);

        if (card.Priority<0)
        {
            Priority.transform.parent.gameObject.SetActive(false);
        }
        if (!show)
        {
            Back.SetActive(true);
        }

        CardGameManager.Instance.MoveCardTo(this, position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardGameManager.Instance.CardClicked(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CardGameManager.Instance.CanFocus(this))
        {
            return;
        }

        CardGameManager.Instance.CardFocused(this);

        _hovered = true;

        Reposition(GetComponentInParent<CardsLayout>());

        _lastSibling = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_lastSibling!=-1)
        {
            transform.SetSiblingIndex(_lastSibling);
        }
        
        _hovered = false;
        Reposition(GetComponentInParent<CardsLayout>());
    }

    public void Reposition(CardsLayout layout)
    {
        Vector3 scale = Vector3.one;
        if (_hovered)
        {
            scale *= 2f;
        }

        MoveCardTo(layout, scale, () => { });
    }

    public Vector3 GetPosition(CardVisual cardVisual, CardsLayout layout)
    {
        if (!layout)
        {
            return cardVisual.transform.position;
        }

        return layout.GetPosition(cardVisual, _hovered);
    }

    public Quaternion GetRotation(CardVisual cardVisual, CardsLayout layout)
    {
        if (!layout)
        {
            return cardVisual.transform.localRotation;
        }

        return layout.GetRotation(cardVisual, _hovered);
    }

    public void MoveCardTo(CardsLayout layout, Action callback = null)
    {
        _lastSibling = -1;
        if (transform.parent!=null && transform.parent.GetComponent<CardsLayout>())
        {
            transform.parent.GetComponent<CardsLayout>().RemoveCardFromLayout(this);
        }

        layout.AddCardToLayout(this);
        MoveCardTo(layout, Vector3.one, callback);
    }

    private void MoveCardTo(CardsLayout parent, Vector3 localScale, Action callback = null)
    {      
        Vector3 localPosition = GetPosition(this, parent);
        Quaternion localRotation = GetRotation(this, parent);

        StopAllCoroutines();
        StartCoroutine(MoveCardToCoroutine(parent.transform, localPosition, localRotation, localScale, callback));
    }

    private void OnDestroy()
    {
        if (GetComponentInParent<CardsLayout>())
        {
            GetComponentInParent<CardsLayout>().RemoveCardFromLayout(this);
        }
    }

    private IEnumerator MoveCardToCoroutine(Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Action callback = null)
    {
        transform.SetParent(parent);
        float time = 0.0f;
        float speed = Mathf.Max(_movementSpeed, _scaleSpeed);

        while (time < speed)
        {
            if (time < _scaleSpeed)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, localScale, time / _scaleSpeed);
            }

            if (time < _movementSpeed)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, localPosition, time / _movementSpeed);
            }

            transform.localRotation = Quaternion.Lerp(transform.localRotation, localRotation, time / speed);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }


        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
        transform.localScale = localScale;

        if (callback != null)
        {
            callback.Invoke();
        }
    }


    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // e.g. store this gameobject as this player's charater in PhotonPlayer.TagObject
        Card c = DefaultResources.GetCardById((int)photonView.InstantiationData[0]);
        bool show = photonView.Owner == PhotonNetwork.LocalPlayer;
        CardGameManager.CardPosition position = (CardGameManager.CardPosition)((int)photonView.InstantiationData[1]);

        Init(c, position, show);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CardGameManager.Instance.DragCard(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CardGameManager.Instance.DragingCardEnded();
    }

    public void OnDrag(PointerEventData eventData)
    {
      
    }
}
