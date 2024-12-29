using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour {

    public event Action<Card> OnDraw;

    [SerializeField] private List<AbilitySO> innitialDeck;
    [SerializeField] private DiscardPile discardPile;
    [SerializeField] private TextMeshProUGUI cardCounterer;
    [SerializeField] private CardFactory cardFactory;

    private CardManager cardManager;
    private RectTransform rectTransform;
    private bool isEmpty = false;
    private bool isInitialized = false;
    private List<Card> cards = new();

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        Hand.OnCardDrawRequest += TryToDraw;
        Innitiate();
    }


    private void Innitiate() {
        foreach (AbilitySO so in innitialDeck) {
            {
                Card card = cardFactory.CreateCard(so, rectTransform);
                cards.Add(card);
            }
        }
        isInitialized = true;
    }

    private void AddNewCard(AbilitySO abilitySO) {
        cards.Add(cardFactory.CreateCard(abilitySO, rectTransform));
    }

    private void Shuffle() {
        System.Random rnd = new System.Random();
        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = rnd.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    private void TryToDraw() {
        if (cards.Count == 0) {
            RenewDeck();
        }

        if (cards.Count > 0) {
            DrawCard();
            isEmpty = false;
        }
        else {
            isEmpty = true;
        }
    }
    private void RenewDeck() {
        cards = discardPile.GetCards();
        foreach (Card card in cards) {
            card.SetParent(rectTransform);
        }
        discardPile.EmptyCards();
        Shuffle();
        UpdateText();
    }


    private void DrawCard() {
        if (cards.Count > 0) {
            OnDraw?.Invoke(cards[0]);
            cards.RemoveAt(0);
            UpdateText();
        }
        else { Debug.LogError("Tring to draw from empty deck"); }
    }

    public bool IsEmpty() {
        return isEmpty;
    }

    public bool IsInitialized() {
        return isInitialized;
    }

    private void UpdateText() {
        cardCounterer.SetText(cards.Count.ToString());
    }

}
