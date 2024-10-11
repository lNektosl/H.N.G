using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {
    private Enemy enemy;
    private IEnemyBaseState currentState;
    [SerializeField] LayerMask playerLayer;

    private void Start() {
        enemy = GetComponent<Enemy>();

        SwitchState(new EnemyPatrolState(enemy, this));
    }

    private void FixedUpdate() {
        if (enemy.IsActive() && enemy.IsReadyToMakeAction()) {
            enemy.SpendEnergy();
            currentState.Execute();
        }
    }

    public void SwitchState(IEnemyBaseState state) {
    currentState = state;
    currentState.EnterState();
    }

}
