using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackProjectile {
    void Innitiate(List<MyTile> tiles, int damage);
}
