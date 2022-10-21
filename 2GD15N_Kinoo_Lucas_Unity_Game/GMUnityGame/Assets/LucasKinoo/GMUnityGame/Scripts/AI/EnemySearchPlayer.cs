using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchPlayer : BaseEnemyAI
{
    // Search for the player like in the game Alien-Isolation
    // The enemy will search the player in a certain range
    // If the player is in the range, the enemy will chase him

    [Header("Search Player")]
    [SerializeField] float searchRange = 10f;
    [SerializeField] float searchDelay = 0.5f;

    float timer = 0f;

    void Update()
    {
        // Check if the player is in the range
        if (Vector3.Distance(transform.position, _player.transform.position) <= searchRange)
        {
            // If the player is in the range, the enemy will chase him
            _navMeshAgent.SetDestination(_player.transform.position);
            _navMeshAgent.isStopped = false;
        }
        else
        {
            // If the player is not in the range, the enemy will search him
            // The enemy will search the player in a certain range
            // If the player is in the range, the enemy will chase him

            // If the timer is less than the search delay
            if (timer < searchDelay)
            {
                // Increase the timer
                timer += Time.deltaTime;
            }
            else
            {
                // Reset the timer
                timer = 0f;

                // If the enemy is not in the search range
                if (_navMeshAgent.remainingDistance <= searchRange)
                {
                    // If the enemy is not in the search range, he will search the player

                    // Create a random position
                    Vector3 randomPosition = new Vector3(Random.Range(-searchRange, searchRange), 0, Random.Range(-searchRange, searchRange));
                    // Set the random position as the destination
                    _navMeshAgent.SetDestination(transform.position + randomPosition);
                    // Enable the agent
                    _navMeshAgent.isStopped = false;
                }
            }
        }
    }
}
