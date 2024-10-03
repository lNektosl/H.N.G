using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscardPile : MonoBehaviour {

    [SerializeField]private TextMeshProUGUI cardCounter;

    private RectTransform rectTransform;
    private List<Card> cards = new();
    

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        CardManager.OnCardActivated += AddToPile;
    }

    private void OnDestroy() {
        CardManager.OnCardActivated -= AddToPile;
    }

    private void AddToPile(Card card) {
        card.SetParent(rectTransform);
        cards.Add(card);
        UpdateText();
    }

    public List<Card> GetCards() {
        return new List<Card>(cards);
    }
    public void EmptyCards() {
        cards.Clear();
        UpdateText();
    }

    private void UpdateText() {
        cardCounter.SetText(cards.Count.ToString());
    }
}
