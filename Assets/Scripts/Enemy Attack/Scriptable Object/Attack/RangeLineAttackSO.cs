using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy/Attack/RangeLineAttack")]
public class RangeLineAttackSO : EnemyAbilitySO, IAttackStrategy, IAttackPositionStratagy {
    [SerializeField] GameObject prefab;
    public int damage;
    public int range;
    public int gap;
    public bool isPiercing;

    public List<MyTile> GetAttackTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {
        List<MyTile> tiles = new();

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            for (int i = gap; i < range + gap; i++) {
                Vector2 vector = new(initialPosition.x, initialPosition.y + (direction.y >= 0 ? i : -i));

                MyTile tile = grid.GetTile(vector);

                if (tile == null || !tile.isAttackable)
                    break;

                tiles.Add(tile);

                if (tile != null && tile.tileType != TileType.FLOOR && !isPiercing)
                    break;

            }
        } else {
            for (int i = gap; i < range + gap; i++) {


                Vector2 vector = new Vector2(initialPosition.x + (direction.x >= 0 ? i : -i), initialPosition.y);

                MyTile tile = grid.GetTile(vector);

                if (tile == null || !tile.isAttackable)
                    break;

                tiles.Add(tile);

                if (tile != null && tile.tileType != TileType.FLOOR && !isPiercing)
                    break;
            }
        }

        return new List<MyTile>(tiles);
    }

    public GameObject GetAttackPrefab() {
        return prefab;
    }

    public List<MyTile> GetAttackPositions(MyTile targetTile, MyGrid grid) {
        List<MyTile> tiles = new List<MyTile>();
        Vector2 targetVector = targetTile.GetVector2PositionWithOffset();
        Debug.Log(targetVector);

        tiles.AddRange(GetTilesFromPosition(new Vector2(targetVector.x, targetVector.y + gap), "y"));
        tiles.AddRange(GetTilesFromPosition(new Vector2(targetVector.x, targetVector.y - gap), "-y"));
        tiles.AddRange(GetTilesFromPosition(new Vector2(targetVector.x + gap, targetVector.y), "x"));
        tiles.AddRange(GetTilesFromPosition(new Vector2(targetVector.x - gap, targetVector.y), "-x"));
        Debug.Log(tiles.Count);
        return new List<MyTile>(tiles);
    }

    private List<MyTile> GetTilesFromPosition(Vector2 position, string dir) {
        List<MyTile> tilesToFind = new List<MyTile>();
        for (int i = gap; i < range + gap; i++) {
            Vector2 currentPosition = dir switch {
                "x" => new Vector2(position.x + i, position.y),
                "-x" => new Vector2(position.x - i, position.y),
                "y" => new Vector2(position.x, position.y + i),
                "-y" => new Vector2(position.x, position.y - i),
                _ => position
            };
            MyTile tile = MyGrid.Instance.GetTile(currentPosition);

            if (tile == null) {
                continue;
            }

            if (tile.tileType != TileType.FLOOR && !isPiercing) {
                tilesToFind.Clear();
                break;
            }

            if (tile.isWalkable) {
                tilesToFind.Add(tile);
            }

        }
        return tilesToFind;
    }

}
