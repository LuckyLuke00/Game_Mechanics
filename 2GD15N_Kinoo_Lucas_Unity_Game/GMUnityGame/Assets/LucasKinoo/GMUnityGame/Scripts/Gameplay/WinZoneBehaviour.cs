using System;
using UnityEngine;

public class WinZoneBehaviour : MonoBehaviour
{
    public static event Action OnWin;
    private bool _hasWon = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasWon)
        {
            Debug.Log("Player has entered the win zone!");
            _hasWon = true;

            OnWin?.Invoke();
        }
    }
}
