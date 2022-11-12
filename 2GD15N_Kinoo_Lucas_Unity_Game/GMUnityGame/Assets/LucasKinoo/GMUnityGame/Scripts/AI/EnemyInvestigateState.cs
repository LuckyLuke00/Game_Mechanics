using UnityEngine;
using UnityEngine.AI;

public class EnemyInvestigateState : EnemyBaseState
{
    private float _investigationTimer = 0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entering Investigate State");
        _investigationTimer = enemy.TimeToKeepChasing;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // Go to the last known position of the target If the target is in sight, go to the chase
        // state If the target is not in sight, wander around the last known position for a while

        if (enemy.PlayerInSight())
        {
            _investigationTimer = enemy.TimeToKeepChasing;
            enemy.SwitchState(enemy._chaseState);
        }

        if (_investigationTimer <= 0f)
        {
            _investigationTimer = enemy.TimeToKeepChasing;
            enemy.SwitchState(enemy._patrolState);
        }

        _investigationTimer -= Time.deltaTime;

        // Predict player direction
        Vector3 playerDirection = enemy.Player.transform.position - enemy.LastKnownLocation;
        Vector3 predictedPlayerPosition = enemy.LastKnownLocation + playerDirection;

        enemy.Agent.destination = predictedPlayerPosition;

        if (enemy.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            enemy.Agent.destination = enemy.Agent.pathEndPosition;
            return;
        }
    }
}