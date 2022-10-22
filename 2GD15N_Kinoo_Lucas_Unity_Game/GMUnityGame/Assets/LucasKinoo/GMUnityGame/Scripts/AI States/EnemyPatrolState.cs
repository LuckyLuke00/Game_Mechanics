using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    // Use waypoints to patrol and randomly choose a waypoint to go to
    private int _destination = 0;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entering Patrol State");
        // Go to closest waypoint
        _destination = GetClosestWaypoint(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // Draw a line to the waypoint
        Debug.DrawLine(enemy.transform.position, enemy.Waypoints[_destination].position, Color.red);

        if (enemy.PlayerInSight())
        {
            enemy.SwitchState(enemy._chaseState);
        }

        // Randomly move around the map
        if (Vector3.Distance(enemy.transform.position, enemy.Waypoints[_destination].position) < .5f)
        {
            _destination = Random.Range(0, enemy.Waypoints.Length);
        }

        enemy.Agent.destination = enemy.Waypoints[_destination].position;
    }
    private int GetClosestWaypoint(EnemyStateManager enemy)
    {
        int closestWaypoint = 0;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < enemy.Waypoints.Length; i++)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.Waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypoint = i;
            }
        }
        return closestWaypoint;
    }
}