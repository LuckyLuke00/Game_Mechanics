using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField] private float _gateSpeed = 1f;

    // Checkbox if the gate should only open when all collectibles are collected
    [SerializeField] private bool _requireAllCollectiblesToOpen = false;

    // When _requireAllCollectiblesToOpen is true disable _collectiblesToOpen
    [SerializeField] private int _collectiblesToOpen = 0;

    private BoxCollider _collider = null;
    private EnemyStateManager _enemy = null;
    private GameManager _gameManager = null;
    private float _gateHeight = 0.0f; // y-Scale

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GateBehaviour: _gameManager is null!");
            return;
        }

        // Get the navmesh agent and check if it exists
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStateManager>();
        if (_enemy == null)
        {
            Debug.LogError("EnemyStateManager is null");
            return;
        }

        _collider = GetComponent<BoxCollider>();
        if (_collider == null)
        {
            Debug.LogError("BoxCollider is null");
            return;
        }

        _collider.enabled = true;

        // Get the gate height: y-Scale
        _gateHeight = transform.localScale.y;
    }

    private void Start()
    {
        if (_requireAllCollectiblesToOpen)
        {
            _collectiblesToOpen = Collectible.Total;
            return;
        }

        if (_collectiblesToOpen > Collectible.Total)
        {
            Debug.LogWarning("GateBehaviour: _collectiblesToOpen is greater than the total number of collectibles!");
            _collectiblesToOpen = Collectible.Total;
        }
    }

    private void Update()
    {
        // If the enemy is in chase mode, move the gate up
        if (_collectiblesToOpen > _gameManager.Collectibles || _enemy.CurrentState == _enemy._chaseState)
        {
            CloseGate();
            return;
        }

        OpenGate();
    }

    private void OpenGate()
    {
        // When the gate is at half the height of the gate, stop moving the gate
        if (transform.position.y > -_gateHeight / 2)
        {
            transform.Translate(Vector3.down * _gateSpeed * Time.deltaTime);
            return;
        }

        // Disable box collider when the gate is down
        _collider.enabled = false;
    }

    private void CloseGate()
    {
        _collider.enabled = true;
        if (transform.position.y < _gateHeight / 2)
        {
            transform.Translate(Vector3.up * _gateSpeed * Time.deltaTime);
        }
    }
}