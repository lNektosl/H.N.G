using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    private Vector2? initialPosition;
    private List<MyTile> tiles = new();

    public List<MyTile> GetTiles(Vector2 initialPosition, Vector2 direction, MyGrid grid) {

        if (this.initialPosition.HasValue && this.initialPosition == initialPosition) return tiles;
        tiles.Clear();

        this.initialPosition = initialPosition;
        
        if(abilitySO is IAttackStrategy strategy)
        tiles = strategy.GetAttackTiles(initialPosition, direction, grid);
        
        return new List<MyTile>(tiles);
    }


    public override void Use() {
        GameObject attackPrefab = ObjectPool.Instance.GetObject();
        IAttackProjectile attack = attackPrefab.GetComponent<IAttackProjectile>();
        if (abilitySO is IAttackAbility attackAbility) {
            int damage = attackAbility.GetDamage();
            attack.Innitiate(tiles, damage);
        } else { Debug.LogError("Ain't attack abillity in attack card"); }
        base.Use();
    }
}
