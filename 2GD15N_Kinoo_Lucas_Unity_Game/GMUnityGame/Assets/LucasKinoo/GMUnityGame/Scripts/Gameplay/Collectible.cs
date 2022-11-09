using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollectibleCollected;

    // Getter setter
    public static int Total { get; set; } = 0;

    private void Awake()
    {
        ++Total;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollectibleCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}