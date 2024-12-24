using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    private Vector2? initialPosition;
    private List<MyTile> tiles = new();
    private AttackCardSO attackCardSO;


    protected override void AddToStart() {
        if (attackCardSO == null && cardSO is AttackCardSO attack) {
            attackCardSO = attack; 
        }
    }

    public List<MyTile> GetTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {

        if (this.initialPosition.HasValue && this.initialPosition == initialPosition) return tiles;
        tiles.Clear();

        this.initialPosition = initialPosition;
        
        if(attackCardSO is IAttackStrategy strategy)
        tiles = strategy.GetAttackTiles(initialPosition, direction, grid);
        
        return new List<MyTile>(tiles);
    }


    public override void Use() {
        GameObject attackPrefab = ObjectPool.Instance.GetObject();
        IAttack attack = attackPrefab.GetComponent<IAttack>();
        attack.Innitiate(tiles,attackCardSO.damage);
        base.Use();
    }
}
