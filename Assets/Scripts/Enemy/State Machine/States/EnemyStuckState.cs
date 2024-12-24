using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStuckState : IEnemyBaseState {
    private bool isAvailable;
    private Enemy enemy;
    private EnemyStateMachine stateMachine;
    private MyGrid grid;
    private List<MyTile> neighbours = new();
    public void EnterState(Enemy enemy, EnemyStateMachine stateMachine) {
        isAvailable = true;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        enemy.awarnes.ResetAwareness();
        grid = MyGrid.Instance;
        Debug.Log("StuckState");
    }

    public void Execute() {
            if (neighbours.Count == 0) {
                neighbours = grid.GetNeighbors(enemy.movement.GetCurrentTile());
            }
            foreach (MyTile tile in neighbours) {
                if (tile != null && tile.IsAvailable()) {
                    UpdateState();
                    return;
                }
            }
            enemy.SpendEnergy();
    }

    public bool IsAvailable() {
        return isAvailable;
    }

    public void UpdateState() {
        isAvailable = false;
        neighbours.Clear();
        enemy.movement.ResetIsStuck();
        stateMachine.SwitchState(stateMachine.patrolState);
    }

    
}
