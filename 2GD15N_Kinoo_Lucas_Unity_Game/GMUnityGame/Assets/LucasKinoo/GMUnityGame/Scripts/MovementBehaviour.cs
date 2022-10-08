using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _movementSpeed = 10;
    
    private bool _isMoving = false;
    private Vector3 _desiredPosition = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;
    public bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }
    
    public Vector3 DesiredPosition
    {
        get { return _desiredPosition; }
        set { _desiredPosition = new Vector3(Mathf.Round(value.x), value.y, Mathf.Round(value.z)); }
}

    public Vector3 StartPosition
    {
        get { return _startPosition; }
        set { _startPosition = new Vector3(Mathf.Round(value.x), value.y, Mathf.Round(value.z)); }
    }

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        if (_isMoving)
        {
            if (Vector3.Distance(_startPosition, transform.position) > 1)
            {
                transform.position = _desiredPosition;
                _isMoving = false;
                return;
            }

            transform.position += (_desiredPosition - _startPosition) * _movementSpeed * Time.deltaTime;
        }
    }
}
