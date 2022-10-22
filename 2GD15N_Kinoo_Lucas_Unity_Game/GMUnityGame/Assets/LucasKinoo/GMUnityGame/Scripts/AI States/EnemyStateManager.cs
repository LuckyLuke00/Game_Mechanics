using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private float _searchRadius = 10f;
    [SerializeField] private float _searchTime = 5f;
    [SerializeField] private Transform[] _Waypoints = null;

    EnemyBaseState _currentState = null;
    public EnemyChaseState _chaseState = new EnemyChaseState();
    public EnemyPatrolState _patrolState = new EnemyPatrolState();
    public EnemyPatrolStateAlt _patrolStateAlt = new EnemyPatrolStateAlt();
    public EnemySearchState _searchState = new EnemySearchState();

    private GameObject _player = null;
    private NavMeshAgent _agent = null;
    private Vector3 _lastKnownLocation = Vector3.zero;

    // Getters and setters
    public float SearchRadius { get => _searchRadius; }
    public float SearchTime { get => _searchTime; }
    public EnemyBaseState CurrentState { get => _currentState; }
    public GameObject Player { get => _player; set => _player = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Transform[] Waypoints { get => _Waypoints; set => _Waypoints = value; }
    public Vector3 LastKnownLocation { get => _lastKnownLocation; set => _lastKnownLocation = value; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();

        // Nullchecks
        if (_player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        if (_agent == null)
        {
            Debug.LogError("NavMeshAgent not found!");
            return;
        }

        // Disable rotation
        _agent.updateRotation = false;

        // Set initial state
        _currentState = _patrolStateAlt;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public bool PlayerInSight()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit) && hit.collider.gameObject.tag == "Player";
    }
}
