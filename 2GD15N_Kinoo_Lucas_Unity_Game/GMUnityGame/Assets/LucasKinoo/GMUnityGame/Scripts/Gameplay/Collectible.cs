using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollectibleCollected;
    public static int _total = 0;

    private void Awake()
    {
        _total++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollectibleCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
