using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour{

    private PlayerInputActions inputActions;
    public event Action OnMovePerformed;
    public event Action OnMoveCanceled;

    public event Action OnSelectButtonPressed;

    private void Awake() {
        inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
    }

    private void Start() {
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_canceled;

        inputActions.Player.Select.performed += Select_performed;
    }

    private void OnDestroy() {
        inputActions.Player.Move.performed -= Move_performed;
        inputActions.Player.Select.performed -= Select_performed;

        inputActions.Player.Select.performed -= Select_performed;
    }

    private void Select_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSelectButtonPressed?.Invoke();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMovePerformed?.Invoke();
    }

    private void Move_canceled(InputAction.CallbackContext context) {
        OnMoveCanceled?.Invoke();
    }

    public Vector2 GetVector2() {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }
}
