using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : BaseEnemyAI
{
    // Reference variables
    [SerializeField] private Transform[] waypoints;

    // Variables to store current waypoint index and current waypoint
    private int _currentWaypointIndex = 0;
    private Transform _currentWaypoint = null;

    protected override void Awake()
    {
        base.Awake();
        
        // Set the current waypoint
        _currentWaypoint = waypoints[_currentWaypointIndex];
    }

    private void Update()
    {
        // Check if the current waypoint is reached
        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 1f)
        {
            //// Choose random waypoint
            //_currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];

            // Increment the current waypoint index
            _currentWaypointIndex++;

            // Check if the current waypoint index is out of bounds
            if (_currentWaypointIndex >= waypoints.Length)
            {
                // Reset the current waypoint index
                _currentWaypointIndex = 0;
            }

            // Set the current waypoint
            _currentWaypoint = waypoints[_currentWaypointIndex];
        }

        // Move the enemy towards the current waypoint
        _navMeshAgent.SetDestination(_currentWaypoint.position);
    }
}
