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

    public void Initialize(AbilitySO abilitySO) {
        if (abilitySO.type == AbilityType.ATTACK) {
        SetAttackCard(abilitySO);
        }
    }

    private void SetAttackCard(AbilitySO abilitySO) {
        nameText.SetText(abilitySO.name);
        descriptionText.SetText(abilitySO.description);
        energyText.SetText(abilitySO.energy.ToString());
        artworkImage.sprite = abilitySO.sprite;

        if (abilitySO is IAttackAbility ability) {
            damageText.SetText(ability.GetDamage().ToString());
        } else {
            Debug.LogError("ain't attack ability in Attack card visualisation");
        }
    }
}
