using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static event Action OnEnemyesTurnEnd;
    public static event Action OnEnemyesTurnStart;



    private int enemyCount = 1;
    private int enemyEndedTurn = 0;
    private void Start() {
        Enemy.OnEnergyZero += EnemyEndTurn;
        TurnManager.OnEnemyTurnStart += EnemyTurnStart;
    }
    private void OnDestroy() {
        Enemy.OnEnergyZero -= EnemyEndTurn;
        TurnManager.OnEnemyTurnStart -= EnemyTurnStart;
    }

    private void EnemyTurnStart() {
        enemyEndedTurn = 0;
        OnEnemyesTurnStart?.Invoke();
    }

    private void EnemyEndTurn() {
        enemyEndedTurn++;
        if (enemyEndedTurn == enemyCount) { 
        EndTurn();
        }
    }

    private void EndTurn() {
        OnEnemyesTurnEnd?.Invoke();
    }

    private void AddEnemy() {
        enemyCount++;
    }
}
