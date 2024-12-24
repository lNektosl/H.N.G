using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy {
    List<MyTile> GetAttackTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid);
    GameObject GetAttackPrefab();
}
