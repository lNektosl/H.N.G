using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PathFinder {

    private MyGrid myGrid;
    List<MyTile> path = new List<MyTile>();
    public PathFinder() {
        myGrid = MyGrid.Instance;
    }
    public List<MyTile> FindPath(MyTile startTile, MyTile destination) {
        if (startTile == null || destination == null || !startTile.isWalkable || !destination.isWalkable) {
            Debug.Log("Invalid start or end tile.");
            return null;
        }

        HashSet<PathNode> openSet = new HashSet<PathNode>();
        HashSet<PathNode> closedList = new HashSet<PathNode>();

        PathNode startNode = new PathNode(startTile) {
            GCost = 0,
            HCost = GetHeuristic(startTile, destination)
        };

        openSet.Add(startNode);

        while (openSet.Count > 0) {
            PathNode currentNode = GetLowestFCostNode(openSet);
            if (currentNode.tile == destination) {
                return ConstructPath(currentNode);
            }

            openSet.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (MyTile neighborTile in myGrid.GetNeighbors(currentNode.tile)) {
                if (!neighborTile.isWalkable)
                    continue;

                PathNode neighborNode = new PathNode(neighborTile);
                if (closedList.Contains(neighborNode))
                    continue;

                float tentativeGCost = currentNode.GCost + neighborTile.moveCost;
                bool isBetterPath = tentativeGCost < neighborNode.GCost;

                if (isBetterPath || !openSet.Contains(neighborNode)) {
                    neighborNode.GCost = tentativeGCost;
                    neighborNode.HCost = GetHeuristic(neighborTile, destination);
                    neighborNode.Parent = currentNode;

                    if (!openSet.Contains(neighborNode)) {
                        openSet.Add(neighborNode);
                    }
                }
            }
        }

        Debug.LogWarning("Path not found.");
        return null;
    }


    private float GetHeuristic(MyTile a, MyTile b) {
        Vector2Int posA = a.GetPosition();
        Vector2Int posB = b.GetPosition();
        return Mathf.Abs(posA.x - posB.x) + Mathf.Abs(posA.y - posB.y);
    }

    private PathNode GetLowestFCostNode(HashSet<PathNode> openSet) {
        PathNode lowestNode = null;

        foreach (PathNode node in openSet) {
            if (lowestNode == null || node.FCost < lowestNode.FCost ||
                (node.FCost == lowestNode.FCost && node.HCost < lowestNode.HCost)) {
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    private List<MyTile> ConstructPath(PathNode endNode) {
        List<MyTile> path = new List<MyTile>();
        PathNode currentNode = endNode;

        while (currentNode != null) {
            path.Add(currentNode.tile);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        path.RemoveAt(0);
        return path;
    }
}
