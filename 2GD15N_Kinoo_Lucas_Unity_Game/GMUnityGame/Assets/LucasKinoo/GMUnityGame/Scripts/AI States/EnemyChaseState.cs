using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.Agent.destination = enemy.LastKnownLocation;

        if (enemy.PlayerInSight())
        {
            enemy.LastKnownLocation = enemy.Player.transform.position;
            return;
        }

        if (Vector3.Distance(enemy.transform.position, enemy.LastKnownLocation) < 1f)
        {
            enemy.SwitchState(enemy._searchState);
        }

    }
}
