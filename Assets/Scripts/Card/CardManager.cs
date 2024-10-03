using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    [SerializeField] private Deck deck;

    public static event Action<Card> OnSelected;
    public static event Action OnUnselected;
    public static event Action<Card> OnHovered;
    public static event Action<Card> OnCardActivated;

    private bool isCardDraged = false;

    private Card selectedCard;
    private EnergyController energyController;

    private CardManager() {
    }

    public static CardManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        Card.OnCardEnter += HighlightTheCard;
        Card.OnCardExit += UnselectCard;
        Card.OnCardDraged += CardDraged;
        Card.OnCardDrop += CardDroped;

        Hand.OnCardSelectedByPress += SelectCardInHand;
        Hand.OnCardUnselect += UnselectCard;

        GamePanel.OnDropOrClickRegistered += ActivateCard;

        deck.OnDraw += UnselectCard;
    }

    private void UnselectCard(Card card) {
        UnselectCard();
    }

    private void OnDestroy() {
        Card.OnCardEnter -= HighlightTheCard;
        Card.OnCardExit -= UnselectCard;
        Card.OnCardDraged -= CardDraged;
        Card.OnCardDrop -= CardDroped;

        Hand.OnCardSelectedByPress -= SelectCardInHand;
        Hand.OnCardUnselect -= UnselectCard;

        GamePanel.OnDropOrClickRegistered -= ActivateCard;

        deck.OnDraw -= UnselectCard;

    }

    private void SelectCardInHand(Card card) {
        HighlightTheCard(card);
        SelectCard(card);
    }

    private void CardDraged(Card card) {
        SelectCard(card);
        isCardDraged = true;
    }

    private void CardDroped() {
        isCardDraged = false;
        UnselectCard();
    }

    private void SelectCard(Card card) {
        selectedCard = card;
        OnSelected?.Invoke(card);
    }

    private void UnselectCard() {
        if (isCardDraged) return;
        selectedCard = null;
        OnUnselected?.Invoke();
    }

    private void HighlightTheCard(Card card) {
        if (isCardDraged) return;
        OnHovered?.Invoke(card);
    }

    private void ActivateCard() {
        if (selectedCard == null || EnergyController.Instance.GetCurrentEnergy() < selectedCard.GetCardSO().energy) return;
        OnCardActivated?.Invoke(selectedCard);
        selectedCard.Use();
        isCardDraged = false;
        UnselectCard();
    }


}

