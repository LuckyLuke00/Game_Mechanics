using System;
using UnityEngine;

public class WinZoneBehaviour : MonoBehaviour
{
    public static event Action OnWin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the win zone!");
            OnWin?.Invoke();

            // Destroy the player
            //Destroy(other.gameObject);
        }
    }
}
