using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private string _defaultSubtitle = "NOT COMPLETED";
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

    public void DisplayBestTime(string bestTime = "")
    {

        if (bestTime == "")
        {
            return;
        }

        _defaultSubtitle = $"BEST: {bestTime}";
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
