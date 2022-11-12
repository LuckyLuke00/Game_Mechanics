using System;
using UnityEngine;

public class WinZoneBehaviour : MonoBehaviour
{
    public static event Action OnWin;

    private bool _hasWon = false;
    private GateBehaviour _winZoneGate = null;

    private void Awake()
    {
        _winZoneGate = GetComponentInChildren<GateBehaviour>();
        if (_winZoneGate == null)
        {
            Debug.LogError("WinZoneBehaviour: _winZoneGate is null!");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasWon)
        {
            Debug.Log("Player has entered the win zone!");
            _hasWon = true;
            
            _winZoneGate.OverrideGate(false);

            OnWin?.Invoke();
        }
    }
}