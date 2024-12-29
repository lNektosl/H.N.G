using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour {
    [SerializeField] private GameObject attackCardPrefab;


    private Dictionary<AbilityType, GameObject> cardPrefabs;


    private void Awake() {
        cardPrefabs = new Dictionary<AbilityType, GameObject>() {
            { AbilityType.ATTACK, attackCardPrefab }
        };
    }

    private GameObject GetCardPrefab(AbilitySO abilitySO) {

        if (cardPrefabs.TryGetValue(abilitySO.type, out GameObject prefab)) {
            return prefab;
        }
        Debug.LogError($"No prefab found for card type: {abilitySO.type}");
        return null;
    }

    public Card CreateCard(AbilitySO carSO, RectTransform rectTransform) {
        GameObject cardPrefab = GetCardPrefab(carSO);
        GameObject cardGameObject = Instantiate(cardPrefab, rectTransform);
        cardGameObject.SetActive(false);
        Card card = cardGameObject.GetComponent<Card>();
        card.SetCartSO(carSO);
        return card;
    }
}
