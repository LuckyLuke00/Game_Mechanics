using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private float _searchRadius = 1f;
    [SerializeField] private float _TimeToKeepChasing = .5f;
    [SerializeField] private Material _GhostMaterial = null;
    [SerializeField] private Transform[] _Waypoints = null;

    EnemyBaseState _currentState = null;
    public EnemyChaseState _chaseState = new EnemyChaseState();
    public EnemyPatrolState _patrolState = new EnemyPatrolState();
    public EnemySearchState _searchState = new EnemySearchState();

    private GameObject _player = null;
    private GameObject _PlayerGhostMesh = null;
    private NavMeshAgent _agent = null;
    private Vector3 _lastKnownLocation = Vector3.zero;

    // Getters and setters
    public EnemyBaseState CurrentState { get => _currentState; }
    public float SearchRadius { get => _searchRadius; }
    public float TimeToKeepChasing { get => _TimeToKeepChasing; }
    public GameObject Player { get => _player; set => _player = value; }
    public GameObject PlayerGhostMesh { get => _PlayerGhostMesh; }
    public Material GhostMaterial { get => _GhostMaterial; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Transform[] Waypoints { get => _Waypoints; set => _Waypoints = value; }
    public Vector3 LastKnownLocation { get => _lastKnownLocation; set => _lastKnownLocation = value; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");

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
        
        _PlayerGhostMesh = new GameObject();
        _PlayerGhostMesh.AddComponent<MeshFilter>().mesh = _player.GetComponentInChildren<MeshFilter>().mesh;
        _PlayerGhostMesh.AddComponent<MeshRenderer>().material = _GhostMaterial;
        _PlayerGhostMesh.transform.localScale = _player.transform.localScale;
        _PlayerGhostMesh.transform.rotation = _player.transform.rotation;
        _PlayerGhostMesh.SetActive(false);

        // Disable rotation
        _agent.updateRotation = false;

        // Set initial state
        _currentState = _patrolState;
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
        if (_player == null) return false;
        
        RaycastHit hit;
        return Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit) && hit.collider.gameObject.tag == "Player";
    }

    public void FadeMesh(MeshRenderer mesh, float duration, float targetAlpha)
    {
        while (mesh.material.color.a != targetAlpha)
        {
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, Mathf.MoveTowards(mesh.material.color.a, targetAlpha, Time.deltaTime / duration));
        }
    }
}
