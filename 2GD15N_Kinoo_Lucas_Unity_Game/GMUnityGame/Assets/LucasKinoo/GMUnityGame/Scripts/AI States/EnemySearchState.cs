using UnityEngine;
using UnityEngine.AI;

public class EnemySearchState : EnemyBaseState
{
    private float _countDown = 0f;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entering Search State");
        _countDown = enemy.SearchTime;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // Keep chasing the player for 5 seconds
        // If the player is not in sight during that time, switch to patrol state
        if (enemy.PlayerInSight())
        {
            _countDown = enemy.SearchTime;
            enemy.SwitchState(enemy._chaseState);
        }
        
        if (_countDown <= 0f || Vector3.Distance(enemy.transform.position, enemy.Agent.destination) < 1f)
        {
            _countDown = enemy.SearchTime;
            enemy.SwitchState(enemy._patrolState);
        }
        
        _countDown -= Time.deltaTime;

        if (enemy.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            enemy.Agent.destination = enemy.Agent.pathEndPosition;
            return;
        }

        enemy.Agent.destination = enemy.LastKnownLocation;

    }
}