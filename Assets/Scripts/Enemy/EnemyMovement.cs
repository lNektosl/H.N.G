using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Enemy enemy;

    private MyTile lastDestination;
    private MyGrid grid;
    private MyTile currentTile;
    private PathFinder pathFinder;
    private List<MyTile> path = new();

    private bool isStuck = false;

    private bool isWaited = false;
    private bool isRetried = false;

    public void Initiate(MyGrid grid, Enemy enemy) {
        this.enemy = enemy;
        this.grid = grid;
        currentTile = grid.GetTile((Vector2)transform.position);
        currentTile.SetIsOccupied(true);
        pathFinder = new();
    }

    public void MoveTo(MyTile destenation) {
        if (lastDestination == null || lastDestination != destenation) {
            if (destenation != null) {
                path = pathFinder.FindPath(currentTile, destenation);
                lastDestination = destenation;
            } else {
                Debug.Log("error");
                enemy.SpendEnergy();
            }
        }

        if (path == null || path.Count == 0) {
            Debug.Log("path == null/0");
            isStuck = true;
            return;
        }

        MyTile nextPositon = path[0];
        if (nextPositon.isOccupied && !isWaited) {
            Debug.Log("waited");
            isWaited = true;
            enemy.Wait();
            return;
        }
        if (IsCanMove(nextPositon)) {
            isRetried = false;
            isStuck = false;
            currentTile.SetIsOccupied(false);
            transform.position = nextPositon.GetVector2PositionWithOffset();
            currentTile = nextPositon;
            currentTile.SetIsOccupied(true);
            path.RemoveAt(0);
            enemy.SpendEnergy();
            isWaited = false;
            return;
        } else if (!isRetried) {
            isRetried = true;
            path = pathFinder.FindPath(currentTile, destenation);
            return;
        }

        isStuck = true;

    }
    public bool IsCanMove(MyTile nextPosition) {

        if (!nextPosition.isWalkable || nextPosition.isOccupied)
            return false;
        return true;
    }



    public MyTile GetCurrentTile() {
        return currentTile;
    }
    public bool IsStuck() {
        return isStuck;
    }
    public void ResetIsStuck() {
        isStuck = false;
    }
}
