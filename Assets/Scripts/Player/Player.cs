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
    private int moveCost = 1;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    private void Start() {
        input.OnMovePerformed += HandleMovment;
    }

    private void OnDisable() {
        input.OnMovePerformed -= HandleMovment;
    }

    private void HandleMovment(Vector2 dir) {
        if (IsCanMove(dir) && EnergyController.Instance.GetCurrentEnergy() >= moveCost) {
            transform.position += (Vector3)dir;
            OnMoved?.Invoke(moveCost);
        }
    }

    private bool IsCanMove(Vector2 dir) {

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, dir, moveDistance, ~mask);

        if (hit.collider != null) {
            return false;
        }
        return true;
    }
}
