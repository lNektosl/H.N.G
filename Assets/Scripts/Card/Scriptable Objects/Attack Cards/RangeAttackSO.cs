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



    public List<Vector2> GetAttackTiles(Vector2 initialPosition, Vector2 direction) {

        List<Vector2> tiles = new();

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            for (int i = gap; i < range + gap; i++) {
                Vector2 tile = new(initialPosition.x, initialPosition.y + (direction.y >= 0 ? i : -i));

                Collider2D collider = Physics2D.OverlapPoint(tile);

                if (collider != null && collider.name == "Wall") break;

                tiles.Add(tile);


                if (collider != null && collider.name != "Wall" && !isPiercing) break;

            }
        }
        else {
            for (int i = 0; i < range; i++) {


                Vector2 tile = new Vector2(initialPosition.x + (direction.x >= 0 ? i : -i), initialPosition.y);


                Collider2D collider = Physics2D.OverlapPoint(tile);

                if (collider != null && collider.name == "Wall") break;

                tiles.Add(tile);

                if (collider != null && collider.name != "Wall" && !isPiercing) break;
            }
        }

        return new List<Vector2>(tiles);
    }

    public GameObject GetAttackPrefab() {
        return prefab;
    }
}
