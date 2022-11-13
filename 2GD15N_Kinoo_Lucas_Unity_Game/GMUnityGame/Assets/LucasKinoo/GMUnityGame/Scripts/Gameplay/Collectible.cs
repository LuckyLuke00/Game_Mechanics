using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupSound = null;
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
            SoundManager._instance.PlaySound(_pickupSound);
            
            OnCollectibleCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}