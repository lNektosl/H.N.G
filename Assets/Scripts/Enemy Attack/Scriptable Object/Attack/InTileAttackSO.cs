using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy/Attack/InTileAttack")]
public class InTileAttackSO : EnemyAbilitySO, IAttackStrategy, IAttackPositionStratagy {
    [SerializeField] GameObject prefab;
    public int damage;
    public int range;

    public List<MyTile> GetAttackTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {
        MyTile tile = grid.GetTile(initialPosition);
        return new List<MyTile> { tile };
    }

    public GameObject GetAttackPrefab() {
        return prefab;
    }

    public List<MyTile> GetAttackPositions(MyTile target, MyGrid grid) {
        List<MyTile> tiles = new List<MyTile>();
        Vector2 targetPosition = target.GetVector2PositionWithOffset();
        for (int i = -range; i <= range; i++) {
            for (int j = -range; j <= range; j++) {
                MyTile tile = grid.GetTile(new Vector2(targetPosition.x + i, targetPosition.y + j));
                if (tile != null && tile.isWalkable) {
                    tiles.Add(tile);
                }
            }
        }
        return new(tiles);
    }
}
