using TMPro;
using UnityEngine;

public class BasicMenu : MonoBehaviour
{
    private const string _defaultSubtitle = "NOT COMPLETED";
    private GameManager _gameManager = null;
    protected TextMeshProUGUI _subtitle = null;
    protected static string _subtitleText = _defaultSubtitle;
    protected Clock _clock = null;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("BasicMenu: _gameManager is null!");
            return;
        }

        _clock = FindObjectOfType<Clock>(true);
        if (_clock == null)
        {
            Debug.LogError("BasicMenu: _clock is null!");
            return;
        }

        // Subtitles are tagged with: "Subtitle"
        _subtitle = GetSubtitle();

        if (_subtitle == null)
        {
            Debug.LogError("BasicMenu: _subtitle text is null!");
            return;
        }

        // Set the subtitle text to the default subtitle
        _subtitle.text = _subtitleText;
    }

    private void Update()
    {
        if (_clock.BestTime < 1) _subtitleText = _defaultSubtitle;
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

    private TextMeshProUGUI GetSubtitle()
    {
        foreach (TextMeshProUGUI title in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (title.CompareTag("Subtitle"))
            {
                return title;
            }
        }
        return null;
    }

    public static void ProgressReset()
    {
        // Update the text when the progress has been reset
        _subtitleText = _defaultSubtitle;
    }

    // Buttons
    public void RestartButton()
    {
        // Restart the game
        GameManager.RestartGame();
    }

    public void ResetProgressButton()
    {
        // Reset the progress
        _gameManager.ResetProgress();
    }

    public void QuitButton()
    {
        // Quit the game
        _gameManager.QuitGame();
    }

    public void Hide()
    { gameObject.SetActive(false); }

    public void Show()
    { gameObject.SetActive(true); }
}