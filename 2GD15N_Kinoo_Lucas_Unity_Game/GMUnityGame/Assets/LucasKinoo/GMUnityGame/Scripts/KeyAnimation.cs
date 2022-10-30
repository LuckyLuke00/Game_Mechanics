using UnityEngine;

public class KeyAnimation : MonoBehaviour
{
    // Rotates the key around its local Y axis at 1 degree per second
    // Makes it float up and down at the same time

    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _floatSpeed = .5f;
    [SerializeField] private float _floatHeight = 0.25f;
    private Vector3 _startPosition = Vector3.zero;
    private void Awake()
    {
        _startPosition = transform.position;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        transform.position = _startPosition + Vector3.up * Mathf.Sin(Time.time * _floatSpeed) * _floatHeight;
    }

}