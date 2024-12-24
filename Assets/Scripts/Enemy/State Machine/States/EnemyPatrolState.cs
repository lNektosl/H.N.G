using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPatrolState : IEnemyBaseState {
    private Enemy enemy;
    private EnemyStateMachine stateMachine;
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine) {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void EnterState() {
    }

    public void Execute() {
        UpdateState();
        
    }
    public void UpdateState() {
        if (enemy.IsAwareOfThePlayer()) {
            stateMachine.SwitchState(new EnemyAgresiveState(enemy, stateMachine ));
        }
    }
}
