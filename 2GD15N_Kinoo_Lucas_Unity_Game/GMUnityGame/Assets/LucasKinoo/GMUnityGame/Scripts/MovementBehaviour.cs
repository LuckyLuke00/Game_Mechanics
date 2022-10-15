using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 10f;

    // Declare reference variables
    private CharacterController _characterController = null;

    // Variables to store player input values
    private Vector3 _currentMovement = Vector3.zero;

    public Vector3 CurrentMovement
    {
        get { return _currentMovement; }
        set { _currentMovement = value; }
    }

    private void Awake()
    {
        // Initially set reference variables
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _characterController.Move(_currentMovement * _movementSpeed * Time.deltaTime);
    }
}
