using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy {
    List<Vector2> GetAttackTiles(Vector2 initialPosition, Vector2 direction);
    GameObject GetAttackPrefab();
}
