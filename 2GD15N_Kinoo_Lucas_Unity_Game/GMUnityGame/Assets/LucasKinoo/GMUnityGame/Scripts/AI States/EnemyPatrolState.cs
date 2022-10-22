using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector3 _destination = Vector3.zero;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entering Patrol State");
        _destination = GenerateRandomDestination(enemy.SearchRadius);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.PlayerInSight())
        {
            enemy.SwitchState(enemy._chaseState);
        }
        
        // Randomly move around the map
        if (Vector3.Distance(enemy.transform.position, _destination) < 1f)
        {
            _destination = GenerateRandomDestination(enemy.SearchRadius);
        }

        enemy.Agent.destination = _destination;

        if (enemy.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            enemy.Agent.destination = enemy.Agent.pathEndPosition;
            _destination = GenerateRandomDestination(enemy.SearchRadius);
        }

    }

    private Vector3 GenerateRandomDestination(float radius)
    {
        // Generate a random point on the map
        // The generated point should be within the bounds of the map
        // The generated point should be on the NavMesh
        // The generated point should be at least 15 units away from the enemy
        // The generated point cannot be in the same position as the previous point

        // Generate random point on the navmesh
        //NavMeshHit hit;
        //Vector3 randomPoint = Random.insideUnitSphere * _patrolRadius;
        //NavMesh.SamplePosition(randomPoint, out hit, _patrolRadius, NavMesh.AllAreas);
        //return hit.position;

        float x = Random.Range(-radius, radius);
        float z = Random.Range(-radius, radius);
        
        return new Vector3(x, 0, z);
    }
}