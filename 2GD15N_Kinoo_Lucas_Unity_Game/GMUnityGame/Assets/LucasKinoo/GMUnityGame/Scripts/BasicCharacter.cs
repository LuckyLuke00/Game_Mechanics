using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    protected MovementBehaviour _movementBehaviour;

    protected virtual void Awake()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }
}
