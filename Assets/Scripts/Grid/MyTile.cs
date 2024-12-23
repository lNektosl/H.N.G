using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyTile {

    public int moveCost;
    private GridManager gridManager;
    private Tilemap tilemap;
    private Vector2Int vector2Int;
    public TileType tileType { get; private set;}

    public bool isWalkable { get; private set; }
    public bool isAttackable { get; private set; }
    public bool isOccupied { get; private set; }





    public MyTile(int x, int y) {
        vector2Int = new Vector2Int(x, y);
        gridManager = GridManager.Instance;
        SetupTile();
    }

    public void SetupTile() {
        (tilemap, tileType) = gridManager.GetTile(vector2Int);
        switch (tileType) {
            case TileType.WALL:
            isWalkable = false;
            isAttackable = false;
            break;

            case TileType.FLOOR:
            isWalkable = true;
            isAttackable = true;
            moveCost = 10;
            break;

            case TileType.VOID:
            isWalkable = false;
            isAttackable = true;
            break;
        }
    }

    public void SetColor(Color color) {
        tilemap.SetColor((Vector3Int)vector2Int, color);
    }

    public void ResetColor() {
        tilemap.SetColor((Vector3Int)vector2Int,Color.white);
    }

    public Vector3 GetVector3PositionWithOffset() {
        return new Vector3(vector2Int.x + 0.5f, vector2Int.y + 0.5f);
    }

    public Vector2 GetVector2PositionWithOffset() {
        return new Vector2(vector2Int.x + 0.5f, vector2Int.y + 0.5f);
    }

    public Vector2Int GetPosition() {
        return vector2Int;
    }

    public void SetIsOccupied(bool isOccupied) { 
    this.isOccupied = isOccupied;
    }

    public bool IsAvailable() {
        return !isOccupied && isWalkable;
    }
}
