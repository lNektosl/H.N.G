using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {
    [SerializeField] private CardSO cardSO;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private Image artworkImage;

    public void Initialize(CardSO cardSO) {
        if (cardSO is AttackCardSO attackCardSO) {
        SetAttackCard(attackCardSO);
        }
    }

    private void SetAttackCard(AttackCardSO cardSO) {
        nameText.SetText(cardSO.cardName);
        descriptionText.SetText(cardSO.description);
        damageText.SetText(cardSO.damage.ToString());
        energyText.SetText(cardSO.energy.ToString());
        artworkImage.sprite = cardSO.sprite;
    }
}
