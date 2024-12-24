using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[CreateAssetMenu(fileName = "New Whide Attack Card", menuName ="Card/Attack Card/Whide Attack")]
public class WhideAttackSO : AttackCardSO, IAttackStrategy {
    [SerializeField] GameObject prefab;

    public int width;
    
    public List<MyTile> GetAttackTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {
        List<MyTile> tiles = new();
        int range = (width - 1) / 2;

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            for (int i = -range; i <= range; i++) {

                Vector2 vector = new Vector2(initialPosition.x + i, initialPosition.y);
                MyTile tile = grid.GetTile(vector);

                if (tile == null || !tile.isAttackable) continue;

                tiles.Add(tile);
            }
        }
        else {
            for (int i = -range; i <= range; i++) {

                Vector2 vector = new Vector2(initialPosition.x, initialPosition.y + i);

                MyTile tile = grid.GetTile(vector);

                if (tile == null || !tile.isAttackable) continue;

                tiles.Add(tile);
            }
        }

        return new List<MyTile>(tiles);
    }

    public GameObject GetAttackPrefab() {
        return prefab;
    }
}
