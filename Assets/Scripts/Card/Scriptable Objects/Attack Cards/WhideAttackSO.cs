using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Whide Attack Card", menuName ="Card/Attack Card/Whide Attack")]
public class WhideAttackSO : AttackCardSO, IAttackStrategy {
    [SerializeField] GameObject prefab;

    public int width;
    
    public List<Vector2> GetAttackTiles(Vector2 initialPosition, Vector2 direction) {
        List<Vector2> tiles = new();
        int range = (width - 1) / 2;

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            for (int i = -range; i <= range; i++) {

                Vector2 tile = new Vector2(initialPosition.x + i, initialPosition.y);

                Collider2D collider = Physics2D.OverlapPoint(tile);
                if (collider != null && collider.name == "Wall") continue;

                tiles.Add(tile);
            }
        }
        else {
            for (int i = -range; i <= range; i++) {

                Vector2 tile = new Vector2(initialPosition.x, initialPosition.y + i);

                Collider2D collider = Physics2D.OverlapPoint(tile);
                if (collider != null && collider.name == "Wall") continue;

                tiles.Add(tile);
            }
        }

        return new List<Vector2>(tiles);
    }

    public GameObject GetAttackPrefab() {
        return prefab;
    }
}
