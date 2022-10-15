using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    // Declare reference variables
    private PlayerControls _playerControls = null;
    
    // Variables to store player input values
    private Vector2 _curentMovementInput = Vector2.zero;
    
    protected override void Awake()
    {
        base.Awake();

        // Initially set reference variables
        _playerControls = new PlayerControls();

        // Set player input callbacks
        _playerControls.CharacterControls.Move.started += OnMovementInput;
        _playerControls.CharacterControls.Move.canceled += OnMovementInput;
        _playerControls.CharacterControls.Move.performed += OnMovementInput;
    }

    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _curentMovementInput = ctx.ReadValue<Vector2>();
        _movementBehaviour.CurrentMovement = new Vector3(_curentMovementInput.x, 0, _curentMovementInput.y);
    }
    private void OnEnable()
    {
        // Enable the character controls action map
        _playerControls.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.CharacterControls.Disable();
    }
}

