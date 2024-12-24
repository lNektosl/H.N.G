using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPatrolState : IEnemyBaseState {
    private Enemy enemy;
    private EnemyStateMachine stateMachine;
    private MyTile targetTile;
    private bool isHaveTarget = false;
    private const int PATROL_RANGE = 5;

    private bool isAvailable;

    public void EnterState(Enemy enemy, EnemyStateMachine stateMachine) {
        isAvailable = true;
        isHaveTarget = false;
        targetTile = null;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        enemy.awarnes.ResetAwareness();
    }

    public void Execute() {
        UpdateState();
        if (isAvailable) {
            Patrol();
        }
    }
    public void UpdateState() {
        if (enemy.movement.IsStuck()) {
            SwitchState(stateMachine.stuckState);
        }
        if (enemy.awarnes.IsAwareOfPlayer()) {
            SwitchState(stateMachine.aggresiveState);
        }
    }

    private void Patrol() {
        if (isHaveTarget) {
            enemy.movement.MoveTo(targetTile);
            if (enemy.movement.GetCurrentTile()==targetTile) {
            isHaveTarget = false;
            }

        } else{ 
            SetNewTarget();
        }
    }
    private void SetNewTarget() {
        isHaveTarget = false;

        List<MyTile> valTiles = new();

        for (int i = -PATROL_RANGE; i <= PATROL_RANGE; i++){
            for (int j = -PATROL_RANGE; j<= PATROL_RANGE; j++) {

                MyTile tile = MyGrid.Instance.GetTile((Vector2)enemy.transform.position + new Vector2(i,j));

                if (tile != null && tile.IsAvailable()) {
                    valTiles.Add(tile);
                }
            }
        }
        if (valTiles.Count == 0) {
            enemy.SpendEnergy();
            return;
        }
        targetTile = valTiles[UnityEngine.Random.Range(0, valTiles.Count)];
        isHaveTarget = true;

        
    }

    private void SwitchState(IEnemyBaseState state) {
        isAvailable = false;
        stateMachine.SwitchState(state);
    }

    public bool IsAvailable() {
       return isAvailable;
    }
}
