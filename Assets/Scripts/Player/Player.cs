using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private PlayerInput input;


    private MyGrid grid;
    private MyTile tile;

    public static event Action<int> OnMoved;

    private float timeGapBetweenMovment = 0.3f;
    private int moveCost = 1;
    private bool isMoving = false;
    private bool isReadyToMove = true;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {    
            Instance = this;
        }
    }

    private void Start() {
        grid = MyGrid.Instance;

        input.OnMovePerformed += StartMovement;
        input.OnMoveCanceled += StopMovement;

        StartCoroutine(AwaitGrid());
    }

    private IEnumerator AwaitGrid() {
        while (!grid.isTilesGenerated) {
            yield return null;
        }
        tile = grid.GetTile((Vector2)transform.position);
        tile.SetIsOccupied(true);

    }

    private void FixedUpdate() {
        if (isMoving && isReadyToMove) {
            HandleMovment();
        }
    }

    private void OnDestroy() {
        input.OnMovePerformed -= StartMovement;
        input.OnMoveCanceled -= StopMovement;
    }

    private void HandleMovment() {
        Vector2 dir = input.GetVector2();

        if (IsCanMove(dir) && EnergyController.Instance.GetCurrentEnergy() >= moveCost) {
            tile.SetIsOccupied(false);
            Vector2 nextPosition = (Vector2)transform.position + dir;
            tile = grid.GetTile(nextPosition);
            tile.SetIsOccupied(true);
            transform.position = tile.GetVector2PositionWithOffset();
            isReadyToMove = false;
            OnMoved?.Invoke(moveCost);
            StartCoroutine(ResetIsReadyToMove());
        }
    }


    private void StartMovement() {
        isMoving = true;
    }
    private void StopMovement() {
        isMoving = false;
    }

    private IEnumerator ResetIsReadyToMove() {
        yield return new WaitForSeconds(timeGapBetweenMovment);
        isReadyToMove = true;
    }

    private bool IsCanMove(Vector2 dir) {



        if (!isReadyToMove)
            return false;
        if (Mathf.Abs(dir.x) == Mathf.Abs(dir.y))
            return false;

        Vector2 nextPosition = (Vector2)transform.position + dir;
        MyTile nextTile = grid.GetTile(nextPosition);
        if (!nextTile.IsAvailable())
            return false;

        return true;
    }
}
