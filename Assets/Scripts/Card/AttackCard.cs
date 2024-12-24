using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    private Vector2? initialPosition;
    private List<MyTile> tiles = new();
    private IAttackStrategy strategy;


    protected override void AddToStart() {
        if (strategy == null && cardSO is IAttackStrategy attack) { 
            strategy = attack; 
        }
    }

    public List<MyTile> GetTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {

        if (this.initialPosition.HasValue && this.initialPosition == initialPosition) return tiles;
        tiles.Clear();

        this.initialPosition = initialPosition;
        tiles = strategy.GetAttackTiles(initialPosition, direction, grid);
        return new List<MyTile>(tiles);
    }


    public override void Use() {
        GameObject attackPrefab = ObjectPool.Instance.GetObject();
        IAttack attack = attackPrefab.GetComponent<IAttack>();
        attack.Innitiate(tiles);
        base.Use();
    }
}
