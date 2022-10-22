using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entering Chase State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.Agent.destination = enemy.LastKnownLocation;

        if (enemy.PlayerInSight())
        {
            enemy.LastKnownLocation = enemy.Player.transform.position;
            return;
        }
        
        enemy.SwitchState(enemy._searchState);

    }
}
