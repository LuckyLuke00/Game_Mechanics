using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField] private float _gateSpeed = 1f;

    private EnemyStateManager _enemy = null;
    private BoxCollider _collider = null;
    private float _gateHeight = 0.0f; // y-Scale

    private void Awake()
    {
        // Get the navmesh agent and check if it exists
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStateManager>();
        _collider = GetComponent<BoxCollider>();
        
        if (_enemy == null)
        {
            Debug.LogError("EnemyStateManager is null");
            return;
        }

        if (_collider == null)
        {
            Debug.LogError("BoxCollider is null");
            return;
        }

        // Get the gate height: y-Scale
        _gateHeight = transform.localScale.y;
    }

    private void Update()
    {
        // When the gate is at half the height of the gate up or down, stop moving the gate

        // If the enemy is in chase mode, move the gate up
        if (_enemy.CurrentState == _enemy._chaseState)
        {
            _collider.enabled = true;
            if (transform.position.y < _gateHeight / 2)
            {
                transform.Translate(Vector3.up * _gateSpeed * Time.deltaTime);
                return;
            }
        }

        // If the enemy is not in chase mode, move the gate down

        if (transform.position.y > -_gateHeight / 2)
        {
            transform.Translate(Vector3.down * _gateSpeed * Time.deltaTime);
            return;
        }

        // Disable box collider when the gate is down
        _collider.enabled = false;
    }
}
