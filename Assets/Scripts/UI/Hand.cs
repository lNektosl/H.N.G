using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {

    public static event Action<Card> OnCardSelectedByPress;
    public static event Action OnCardUnselect;
    public static event Action OnCardDrawRequest;

    [SerializeField] private PlayerInput input;
    [SerializeField] private CardFactory cardFactory;
    [SerializeField] private Deck deck;

    private List<Card> cards = new();
    private RectTransform rectTransform;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    private int cardWidth = 250;
    private int handWidth = 1500;
    private int maxCardsForWidth = 6;
    private int selectedCardInArray = 0;
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        deck.OnDraw += CardToHand;
        CardManager.OnCardActivated += RemoveCardFromHand;
        input.OnSelectButtonPressed += SelectNextCard;
        StartCoroutine(WaitForInitialization());
    }

    private void OnDestroy() {
        deck.OnDraw -= CardToHand;
        CardManager.OnCardActivated -= RemoveCardFromHand;
        input.OnSelectButtonPressed -= SelectNextCard;
    }

    private IEnumerator WaitForInitialization() {
        while (!deck.IsInitialized()) {
            yield return null;
        }
        Inniate();
    }
    private void Inniate() {
        for (int i = 0; i<maxCardsForWidth;i++) {
            if (deck.IsEmpty()) break;
            OnCardDrawRequest?.Invoke();
        }
    }

    private void SelectNextCard() {
        if (cards.Count <= 0) return;
        selectedCardInArray++;
        if (selectedCardInArray > cards.Count) {
            OnCardUnselect?.Invoke();
            selectedCardInArray = 0;
        }
        else {
            Card card = cards[selectedCardInArray - 1];
            OnCardSelectedByPress?.Invoke(card);
        }
    }

    private void SendDrawRequest() {

    }

    private void CardToHand(Card card) {
        cards.Add(card);
        card.SetParent(rectTransform);
        card.Activate();
        CalculateSpasing();
    }

    private void RemoveCardFromHand(Card card) {
        cards.Remove(card);
        CalculateSpasing();
        selectedCardInArray = 0;
    }

    private void CalculateSpasing() {
        if (cards.Count > maxCardsForWidth) {
            horizontalLayoutGroup.spacing = -((cardWidth * cards.Count) - handWidth) / (cards.Count - 1);
        }
        else {
            horizontalLayoutGroup.spacing = 0;
        }
    }


}
