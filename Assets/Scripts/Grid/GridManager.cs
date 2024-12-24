using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;


public class GridManager : MonoBehaviour {

    public static GridManager Instance { get; private set; }

    [SerializeField] private Tilemap wallMap;
    [SerializeField] private Tilemap floorMap;
    [SerializeField] private LayerMask mask;



    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    public (Tilemap, TileType) GetTile(Vector2Int vector2Int) {
        Collider2D collider = Physics2D.OverlapPoint(new Vector2(vector2Int.x+0.5f, vector2Int.y + 0.5f),mask);

        if (collider != null) {
            switch(collider.name){
                case "Wall":
                    return (wallMap,TileType.WALL);
                case "Floor":
                return (floorMap,TileType.FLOOR);
            }
                
        }

        return (wallMap,TileType.VOID);
    }
}
