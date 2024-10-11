using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private PlayerInput input;
    [SerializeField] private LayerMask mask;

    public static event Action<int> OnMoved;

    private float moveDistance = 1f;
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
        input.OnMovePerformed += StartMovement;
        input.OnMoveCanceled += StopMovement;
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
            transform.position += (Vector3)dir;
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
        if (dir.x == dir.y)
            return false;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, dir, moveDistance, ~mask);

        if (hit.collider != null) {
            return false;
        }
        return true;
    }
}
