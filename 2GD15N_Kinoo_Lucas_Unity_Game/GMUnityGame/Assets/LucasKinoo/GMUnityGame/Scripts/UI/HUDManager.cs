using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // HUD Elements
    private CollectibleCount _collectibleCount = null;
    private Clock _clock = null;

    private void Awake()
    {
        // Always in children
        _collectibleCount = GetComponentInChildren<CollectibleCount>();
        if (_collectibleCount == null)
        {
            Debug.LogError("HUDManager: _collectibleCount is null!");
            return;
        }

        _clock = GetComponentInChildren<Clock>();
        if (_clock == null)
        {
            Debug.LogError("HUDManager: _clock is null!");
            return;
        }
    }

    public void ToggleHud()
    {
        // Check if this object is active
        if (gameObject.activeSelf)
        {
            // If it is, disable it
            gameObject.SetActive(false);
        }
        else
        {
            // If it isn't, enable it
            gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        MenuManager.OnMenuActive += ToggleHud;
    }

    private void OnDisable()
    {
        MenuManager.OnMenuActive -= ToggleHud;
    }
}
