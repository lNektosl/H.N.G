using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour {
    [SerializeField] private GameObject attackCardPrefab;


    private Dictionary<System.Type, GameObject> cardPrefabs;


    private void Awake() {
        cardPrefabs = new Dictionary<System.Type, GameObject>() {
            { typeof(AttackCardSO), attackCardPrefab }
        };
    }

    private GameObject GetCardPrefab(CardSO carSO) {
        System.Type type = carSO.GetType().BaseType;

        if (cardPrefabs.TryGetValue(type, out GameObject prefab)) {
            return prefab;
        }
        Debug.LogError($"No prefab found for card type: {type}");
        return null;
    }

    public Card CreateCard(CardSO carSO, RectTransform rectTransform) {
        GameObject cardPrefab = GetCardPrefab(carSO);
        GameObject cardGameObject = Instantiate(cardPrefab, rectTransform);
        cardGameObject.SetActive(false);
        Card card = cardGameObject.GetComponent<Card>();
        card.SetCartSO(carSO);
        return card;
    }
}
