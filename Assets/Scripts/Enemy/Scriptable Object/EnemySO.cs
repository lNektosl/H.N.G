using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy/Enemy")]
public class EnemySO : ScriptableObject {

    public string enemyName;
    public int energy;
    public int hp;
    public float baseAwarnes;
    public List<AbilitySO> attacks = new();

    public Dictionary<AbilitySO, List<MyTile>> GetAttackAndTiles(MyTile target, MyGrid grid) {
        Dictionary<AbilitySO, List<MyTile>> attacksDic = new();
        foreach (var attack in attacks) {
            if (attack is IAttackPositionStratagy stratagy) {
                attacksDic[attack] = stratagy.GetAttackPositions(target, grid);
            }
        }
        return attacksDic;
    }
}
