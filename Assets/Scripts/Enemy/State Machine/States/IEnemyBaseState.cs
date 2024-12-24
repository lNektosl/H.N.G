using UnityEngine;

public interface IEnemyBaseState {

    void EnterState(Enemy enemy,EnemyStateMachine stateMachine);
    void UpdateState();
    void Execute();

    bool IsAvailable();
}
