using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackPositionStratagy {
    List<MyTile> GetAttackPositions(MyTile targetTile,MyGrid grid);
}
