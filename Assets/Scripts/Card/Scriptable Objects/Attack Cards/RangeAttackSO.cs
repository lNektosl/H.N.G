using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Range Attack Card", menuName = "Card/Attack Card/Range Attack")]
public class RangeAttackSO : AttackCardSO, IAttackStrategy {
    [SerializeField] private GameObject prefab;

    public int range;
    public int gap;
    public bool isPiercing;



    public List<MyTile> GetAttackTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {

        List<MyTile> tiles = new();

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            for (int i = gap; i < range + gap; i++) {
                Vector2 vector = new(initialPosition.x, initialPosition.y + (direction.y >= 0 ? i : -i));

                MyTile tile = grid.GetTile(vector);

                if (tile == null || !tile.isAttackable) break;

                tiles.Add(tile);

                if (tile != null && tile.tileType != TileType.FLOOR && !isPiercing) break;

            }
        }
        else {
            for (int i = 0; i < range; i++) {


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
}
