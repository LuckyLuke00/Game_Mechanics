using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BaseEnemyAI : MonoBehaviour
{
    protected NavMeshAgent _navMeshAgent = null;
    protected GameObject _player = null;

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        _navMeshAgent.updateRotation = false;
    }

}
