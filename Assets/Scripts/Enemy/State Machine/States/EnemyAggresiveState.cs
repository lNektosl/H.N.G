using System.Collections.Generic;
using UnityEngine;

public class EnemyAggresiveState : IEnemyBaseState {
    private Enemy enemy;
    private EnemyStateMachine stateMachine;
    private MyTile closestTile;
    private MyTile lastPlayerTile;
    private float awarnesModifier = 2f;
    private EnemyAbilitySO currentStratagy;

    private bool isAvailable;
    private bool isUsedAbility;
    public void EnterState(Enemy enemy, EnemyStateMachine stateMachine) {
        isAvailable = true;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        enemy.awarnes.SetAwarnesModifier(awarnesModifier);
    }

    public void Execute() {
        UpdateState();
        if (isAvailable) {
            isUsedAbility = false;
            if (lastPlayerTile == null || lastPlayerTile != enemy.awarnes.GetPlayerPosition()
                || closestTile != null && closestTile.isOccupied && closestTile != enemy.movement.GetCurrentTile()) {
                lastPlayerTile = enemy.awarnes.GetPlayerPosition();
                ChoseAttackPosition();
            }
            MoveToAttackPosition();
        }
    }

    public bool IsAvailable() {
        return isAvailable;
    }

    public void UpdateState() {
        if (enemy.movement.IsStuck()) {
            SwitchState(stateMachine.stuckState);
        }
        if (enemy.awarnes.IsAwareOfPlayer() && !enemy.awarnes.IsPlayerVisible()) {
            SwitchState(stateMachine.chaseState);
        }
    }

    private void ChoseAttackPosition() {
        Dictionary<EnemyAbilitySO, List<MyTile>> attacks = enemy.so.GetAttackAndTiles(enemy.awarnes.GetPlayerPosition(), MyGrid.Instance);
        float closestDistance = float.MaxValue;
        foreach (var entry in attacks) {
            foreach (MyTile tile in entry.Value) {
                float distance = Vector2.Distance(enemy.transform.position, tile.GetVector2PositionWithOffset());
                if (enemy.movement.GetCurrentTile() == tile) {
                    closestTile = tile;
                    return;
                }
                if (distance < closestDistance) {
                    currentStratagy = entry.Key;
                    closestDistance = distance;
                    closestTile = tile;
                }
            }
        }
    }

    private void MoveToAttackPosition() {
        if (closestTile == enemy.movement.GetCurrentTile()) {
            Attack();
            return;
        }
        enemy.movement.MoveTo(closestTile);

    }
    private void Attack() {
        if (enemy.isEnoughEnergy(currentStratagy.energy) && !isUsedAbility) {
            if (currentStratagy is IAttackStrategy strategy) {

                Vector2 dir = (lastPlayerTile.GetPosition() - enemy.movement.GetCurrentTile().GetPosition());
                dir = dir.normalized;

                List<MyTile> tiles = strategy.GetAttackTiles(enemy.movement.GetCurrentTile().GetVector2PositionWithOffset()+dir, dir, MyGrid.Instance);
                GameObject attackPrefab = ObjectPool.Instance.GetObject();
                
                IAttackProjectile attack = attackPrefab.GetComponent<IAttackProjectile>();
                if (currentStratagy is IAttackAbility ab) {
                    attack.Innitiate(tiles, ab.GetDamage()); 
                }
                isUsedAbility = true;
                enemy.SpendEnergy(currentStratagy.energy);

            }
        } else {
            Debug.Log("Not enough energy");
            enemy.SpendEnergy();
        }
    }

    private void SwitchState(IEnemyBaseState state) {
        isAvailable = false;
        stateMachine.SwitchState(state);
    }
}
