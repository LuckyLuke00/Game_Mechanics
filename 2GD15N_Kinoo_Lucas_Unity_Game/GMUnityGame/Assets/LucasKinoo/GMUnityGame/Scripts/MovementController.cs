using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    // Declare reference variables
    private PlayerControls _playerControls = null;
    private CharacterController _characterController = null;
    private Rigidbody _rigidBody = null;

    // Variables to store player input values
    private Vector2 _curentMovementInput = Vector2.zero;
    private Vector3 _currentMovement = Vector3.zero;
    private bool _isMovementPressed = false;

    private void Awake()
    {
        // Initially set reference variables
        _playerControls = new PlayerControls();
        _characterController = GetComponent<CharacterController>();
        _rigidBody = GetComponent<Rigidbody>();

        // Set player input callbacks
        _playerControls.CharacterControls.Move.started += OnMovementInput;
        _playerControls.CharacterControls.Move.canceled += OnMovementInput;
        _playerControls.CharacterControls.Move.performed += OnMovementInput;
    }
    private void Update()
    {
        _characterController.Move(_currentMovement * movementSpeed * Time.deltaTime);
    }
    
    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _curentMovementInput = ctx.ReadValue<Vector2>();
        _currentMovement.x = _curentMovementInput.x;
        _currentMovement.z = _curentMovementInput.y;
        _isMovementPressed = _curentMovementInput.x != 0 || _curentMovementInput.y != 0;
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
