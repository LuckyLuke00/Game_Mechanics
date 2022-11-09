using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    [SerializeField] private string _collectibleName = "Collectible";
    
    private int _count = 0;
    TMPro.TMP_Text _text = null;

    private void Awake()
    {
        // The text component is a child of this object
        _text = GetComponentInChildren<TMPro.TMP_Text>();

        if (_text == null)
        {
            Debug.LogError("CollectibleCount: Text is null!");
        }
    }

    private void Start()
    {
        // Needs to be called in start because the collectibles are instantiated at runtime
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
        _text.text = $"{_collectibleName}: [ {_count} / {Collectible._total} ]";
    }
}
