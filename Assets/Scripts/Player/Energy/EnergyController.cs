using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour {

    public static event Action OnEnergyIsZero;
    public static event Action OnEnergyChanged;

    public static EnergyController Instance { get; private set; }

    private int baseEnergy = 3;
    private int currentEnergy;

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
        }
        else { Instance = this; }
    }

    private void Start() {
        ResetEnergy();

        Player.OnMoved += SpendEnergy;
        CardManager.OnCardActivated += (card) => SpendEnergy(card.GetCardSO().energy);
    }

    private void AddEnergy(int energy) {
        currentEnergy += energy;
        OnEnergyChanged?.Invoke();
    }

    private void SpendEnergy(int energy) {
        if (currentEnergy - energy < 0) return;

        currentEnergy -= energy;

        if (currentEnergy == 0) EnergyReachedZero();

        OnEnergyChanged?.Invoke();
    }

    private void ResetEnergy() {
        currentEnergy = baseEnergy;

        OnEnergyChanged?.Invoke();
    }

    private void EnergyReachedZero() {
        OnEnergyIsZero?.Invoke();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ResetEnergy();
        }
    }

    public int GetCurrentEnergy() {
        return currentEnergy;
    }
}
