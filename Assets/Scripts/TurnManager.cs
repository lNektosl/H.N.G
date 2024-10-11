using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    public static event Action OnEnemyTurnStart;
    public static event Action OnPlayerTurnStart;

    private float timeBetweenTurns = 0.7f;

    private Action onEnergyIsZeroHandler;
    private Action onEnemiesTurnEndHandler;


    private void Start() {
        onEnergyIsZeroHandler = () => StartCoroutine(StartEnemyTurn());
        onEnemiesTurnEndHandler = () => StartCoroutine(StartPlayerTurn());

        EnergyController.OnEnergyIsZero += onEnergyIsZeroHandler;
        EnemyManager.OnEnemyesTurnEnd += onEnemiesTurnEndHandler;
    }
    private void OnDestroy() {
        EnergyController.OnEnergyIsZero -= onEnergyIsZeroHandler;
        EnemyManager.OnEnemyesTurnEnd -= onEnemiesTurnEndHandler;
    }
    private IEnumerator StartEnemyTurn() {
        yield return new WaitForSeconds(timeBetweenTurns);
        OnEnemyTurnStart?.Invoke();
    }
    private IEnumerator StartPlayerTurn() {
        yield return new WaitForSeconds(timeBetweenTurns);
        OnPlayerTurnStart?.Invoke();
    }
}
