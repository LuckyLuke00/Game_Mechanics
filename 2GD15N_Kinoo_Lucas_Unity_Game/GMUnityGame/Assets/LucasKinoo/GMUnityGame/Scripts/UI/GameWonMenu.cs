using UnityEngine;
using TMPro;

public class GameWonMenu : MonoBehaviour
{
    [SerializeField] private const string _defaultSubtitle = "NOT COMPLETED";
    [SerializeField] private TextMeshProUGUI _subtitle = null;
    private string _subtitleText = "";

    private void Awake()
    {
        if (_subtitle == null)
        {
            // Log an error
            Debug.LogError("GameOverMenu: Subtitle text is null!");
            return;
        }

        // Set the subtitle text to the default subtitle
        _subtitleText = _defaultSubtitle;
        _subtitle.text = _subtitleText;
    }
    private void Update()
    {
        _subtitle.text = _subtitleText;
    }

    private void OnEnable()
    {
        GameManager.OnResetProgress += ProgressReset;
    }

    private void OnDisable()
    {
        GameManager.OnResetProgress -= ProgressReset;
    }

    // Display the time it took to complete the level
    public void DisplayTime(string time = "")
    {
        if (time == "")
        {
            // Error
            _subtitleText = "TIME NOT FOUND";
        }

        _subtitleText = $"COMPLETED IN {time}";
    }
    public void Hide()
    {
        // Hide the game over menu
        gameObject.SetActive(false);
    }
    public void Show()
    {
        // Show the game over menu
        gameObject.SetActive(true);
    }

    public void ProgressReset()
    {
        _subtitleText = _defaultSubtitle;
    }
}
