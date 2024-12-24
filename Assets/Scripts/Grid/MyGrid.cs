using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour {

    [SerializeField] private int xRange;
    [SerializeField] private int yRange;
    private MyTile[,] gridArray;
    public bool isTilesGenerated { get; private set; }

    public static MyGrid Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
        isTilesGenerated = false;
    }

    private void Start() {
        int width = xRange * 2;
        int height = yRange * 2;
        gridArray = new MyTile[width, height];
        CreateGrid();
    }

    private void CreateGrid() {
        for (int x = -xRange; x < xRange; x++) {
            for (int y = -yRange; y < yRange; y++) {
                gridArray[x + xRange, y + yRange] = new MyTile(x, y);
            }
        }

        isTilesGenerated = true;

    }

    public MyTile GetTile(Vector2 vector2) {

        int x = Mathf.FloorToInt(vector2.x);
        int y = Mathf.FloorToInt(vector2.y);

        if (x >= -xRange && x < xRange && y >= -yRange && y < yRange) {
            return gridArray[x + xRange, y + yRange];
        }
        return null;
    }

    public List<MyTile> GetNeighbors(MyTile tile) {
        List<MyTile> neighbors = new List<MyTile>();
        Vector2Int[] directions = {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
        };

        foreach (Vector2Int direction in directions) {
            MyTile neighbor = GetTile(new Vector2(tile.GetVector2PositionWithOffset().x, tile.GetVector2PositionWithOffset().y) + (Vector2)direction);
            if (neighbor != null && neighbor.isWalkable && !neighbor.isOccupied) {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }
}
