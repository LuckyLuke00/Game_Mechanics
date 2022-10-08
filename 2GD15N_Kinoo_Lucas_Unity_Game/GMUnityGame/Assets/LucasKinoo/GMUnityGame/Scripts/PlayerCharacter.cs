using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BasicCharacter
{
    const string MOVEMENT_HORIZONTAL = "MovementHorizontal";
    const string MOVEMENT_VERTICAL = "MovementVertical";

    private void Update()
    {
        HandleMovementInput();
    }
    
    void HandleMovementInput()
    {
        if (_movementBehaviour == null)
            return;

        float horizontal = Input.GetAxisRaw(MOVEMENT_HORIZONTAL);
        float vertical = Input.GetAxisRaw(MOVEMENT_VERTICAL);

        // Cant change direction when not finished moving
        if (_movementBehaviour.IsMoving)
            return;

        if (horizontal != 0)
        {
            _movementBehaviour.DesiredPosition = transform.position + Vector3.right * horizontal;
            _movementBehaviour.StartPosition = transform.position;
            _movementBehaviour.IsMoving = true;
        }
        
        if (vertical != 0)
        {
            _movementBehaviour.DesiredPosition = transform.position + Vector3.forward * vertical;
            _movementBehaviour.StartPosition = transform.position;
            _movementBehaviour.IsMoving = true;
        }
    }
}
