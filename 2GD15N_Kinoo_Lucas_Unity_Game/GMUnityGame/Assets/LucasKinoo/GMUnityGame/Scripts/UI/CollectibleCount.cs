using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    int _count = 0;
    TMPro.TMP_Text _text = null;
    
    private void Awake()
    {
        _text = GetComponent<TMPro.TMP_Text>();

        if (_text == null)
        {
            Debug.LogError("CollectibleCount: Text is null!");
        }
    }

    private void Start()
    {
        UpdateCount();
    }

    private void OnEnable()
    {
        Collectible.OnCollectibleCollected += OnCollectibleCollected;
    }
    private void OnDisable()
    {
        Collectible.OnCollectibleCollected -= OnCollectibleCollected;
    }

    private void OnCollectibleCollected()
    {
        ++_count;
        UpdateCount();
    }

    void UpdateCount()
    {
        // Set only the number Collectible._total color to _hexColor
        _text.text = $"Keys: [ {_count} / {Collectible._total} ]";
    }
}
