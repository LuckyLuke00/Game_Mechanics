using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameOverMenu _gameOverMenu = null;
    [SerializeField] private GameWonMenu _gameWonMenu = null;
    private Clock _clock = null;

    private void Awake()
    {
        if (_gameOverMenu == null)
        {
            Debug.LogError("UIManager: Game over menu is null!");
        }

        if (_gameWonMenu == null)
        {
            Debug.LogError("UIManager: Game won menu is null!");
        }

        _clock = FindObjectOfType<Clock>();

        if (_clock == null)
        {
            Debug.LogError("UIManager: Clock is null!");
        }

        _gameOverMenu.Hide();
        _gameWonMenu.Hide();
    }

    private void OnEnable()
    {
        PlayerCharacter.OnPlayerDeath += OnPlayerDeath;
        WinZoneBehaviour.OnWin += OnWin;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnPlayerDeath -= OnPlayerDeath;
        WinZoneBehaviour.OnWin -= OnWin;
    }

    private void OnWin()
    {
        _gameWonMenu.DisplayTime(_clock.GetTimeText(_clock.CurrentTime));
        _gameWonMenu.Show();

        _clock.Hide();
        _clock.IsPaused = true;
    }

    public void OnPlayerDeath()
    {
        _gameOverMenu.DisplayBestTime(_clock.BestTime > 0f ? _clock.GetTimeText(_clock.BestTime) : "");
        _gameOverMenu.Show();
        
        _clock.Hide();
        _clock.IsPaused = true;
    }
}
