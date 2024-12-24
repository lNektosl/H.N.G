using UnityEngine;

public class EnemyAgresiveState : IEnemyBaseState {
    private Enemy enemy;
    private EnemyStateMachine stateMachine;
    public EnemyAgresiveState(Enemy enemy, EnemyStateMachine stateMachine) {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void EnterState() {
        Debug.Log("I'm now aggresive");
    }

    public void Execute() {
        throw new System.NotImplementedException();
    }

    public void UpdateState() {
        throw new System.NotImplementedException();
    }
}
