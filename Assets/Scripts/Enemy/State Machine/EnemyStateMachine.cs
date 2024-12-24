using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {
    private Enemy enemy;
    private IEnemyBaseState currentState;
    [SerializeField] LayerMask playerLayer;

    public EnemyPatrolState patrolState { get; private set; }
    public EnemyAggresiveState aggresiveState {  get; private set; }
    public EnemyChaseState chaseState { get; private set; }
    public EnemyStuckState stuckState { get; private set; }

    private void Start() {
        patrolState = new EnemyPatrolState();
        aggresiveState = new EnemyAggresiveState();
        chaseState = new EnemyChaseState();
        stuckState = new EnemyStuckState();

        enemy = GetComponent<Enemy>();

        SwitchState(patrolState);
    }

    private void FixedUpdate() {
        if (enemy.IsActive() && enemy.IsReadyToMakeAction() && currentState.IsAvailable()) {
            currentState.Execute();
        }
    }

    public void SwitchState(IEnemyBaseState state) {
    currentState = state;
    currentState.EnterState(enemy, this);
    }

}
