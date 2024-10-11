using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyVisualiser : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI energyCounter;

    private void Start() {
        EnergyController.OnEnergyChanged += UpdateEnergy;
        UpdateEnergy();
    }
    private void OnDestroy() {
        EnergyController.OnEnergyChanged -= UpdateEnergy;
    }

    private void UpdateEnergy() {
        energyCounter.SetText(EnergyController.Instance.GetCurrentEnergy().ToString());
    }
}
