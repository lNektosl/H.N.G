using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;

public class EnemyChaseState : IEnemyBaseState {
    private Enemy enemy;
    private EnemyStateMachine stateMachine;

    private float awarnesModifier = 1.5f;

    private MyTile lastPlayerPosition;

    private bool isAvailable;
    public void EnterState(Enemy enemy, EnemyStateMachine stateMachine) {
        isAvailable = true;
        this.enemy = enemy;
        Debug.Log(enemy.name);
        this.stateMachine = stateMachine;
        enemy.awarnes.SetAwarnesModifier(awarnesModifier);
        lastPlayerPosition = enemy.awarnes.GetPlayerPosition();
    }

    public void Execute() {
        UpdateState();
        if (isAvailable) {
            enemy.movement.MoveTo(lastPlayerPosition);
        }
    }

    public bool IsAvailable() {
        return isAvailable;
    }

    public void UpdateState() {
        if (enemy.movement.IsStuck()) {
            SwitchState(stateMachine.stuckState);
        }
        if (enemy.awarnes.IsPlayerVisible() && enemy.awarnes.IsAwareOfPlayer()) {
            SwitchState(stateMachine.aggresiveState);
        }
        // Если достигли последней известной позиции игрока или она занята и игрока не видно
        if (enemy.movement.GetCurrentTile() == lastPlayerPosition && !enemy.awarnes.IsPlayerVisible() ||
            lastPlayerPosition.isOccupied && !enemy.awarnes.IsPlayerVisible()
            && Vector2.Distance(enemy.movement.GetCurrentTile().GetVector2PositionWithOffset(),
                                lastPlayerPosition.GetVector2PositionWithOffset()) <= 2) {
            SwitchState(stateMachine.patrolState);
        }
    }


    private void SwitchState(IEnemyBaseState state) {
        isAvailable = false;
        stateMachine.SwitchState(state);
    }
}
