using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{

    private PlayerInputActions inputActions;
    public event Action<Vector2> OnMovePerformed;
    public event Action OnSelectButtonPressed;

    private void Awake() {
        inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
    }

    private void Start() {
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Select.performed += Select_performed;
    }

    private void OnDestroy() {
        inputActions.Player.Move.performed -= Move_performed;
        inputActions.Player.Select.performed -= Select_performed;
    }

    private void Select_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSelectButtonPressed?.Invoke();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMovePerformed?.Invoke(inputActions.Player.Move.ReadValue<Vector2>());
    }

}
