using UnityEngine;
using TMPro;

public class GameWonMenu : MonoBehaviour
{
    [SerializeField] private string _defaultSubtitle = "COMPLETED IN X:XX";
    [SerializeField] private TextMeshProUGUI _subtitleText = null;

    private void Awake()
    {
        if (_subtitleText == null)
        {
            // Log an error
            Debug.LogError("GameOverMenu: Subtitle text is null!");
            return;
        }

        // Set the subtitle text to the default subtitle
        _subtitleText.text = _defaultSubtitle;
    }
    private void Update()
    {
        _subtitleText.text = _defaultSubtitle;
    }

    // Display the time it took to complete the level
    public void DisplayTime(string time = "")
    {
        if (time == "")
        {
            // Error
            _defaultSubtitle = "TIME NOT FOUND";
        }

        _defaultSubtitle = $"COMPLETED IN {time}";
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
}
