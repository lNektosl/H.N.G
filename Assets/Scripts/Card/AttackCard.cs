using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    private Vector2? initialPosition;
    private List<Vector2> tiles = new();
    private IAttackStrategy strategy;


    protected override void AddToStart() {
        if (strategy == null && cardSO is IAttackStrategy attack) { 
            strategy = attack; 
        }
    }

    public List<Vector2> GetTiles(Vector2 initialPosition, Vector2 direction) {

        if (this.initialPosition.HasValue && this.initialPosition == initialPosition) return tiles;
        tiles.Clear();

        this.initialPosition = initialPosition;
        tiles = strategy.GetAttackTiles(initialPosition, direction);
        return new List<Vector2>(tiles);
    }


    public override void Use() {
        GameObject attackPrefab = Instantiate(strategy.GetAttackPrefab(), (Vector3)tiles[0], Quaternion.identity);
        IAttack attack = attackPrefab.GetComponent<IAttack>();
        attack.Innitiate(tiles);
        base.Use();
    }
}
