using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Clock _clock = null;
    private int _collectibles = 0;
    
    public static event Action OnResetProgress;

    //Getter for _collectibles
    public int Collectibles { get => _collectibles; }

    private void Awake()
    {
        _clock = FindObjectOfType<Clock>();

        if (_clock == null)
        {
            Debug.LogError("GameManager: _clock is null!");
        }
        
        LoadHighscores();
    }

    private void OnEnable()
    {
        WinZoneBehaviour.OnWin += SaveHighscores;
        PlayerCharacter.OnPlayerDeath += _clock.PauseTimer;
        Collectible.OnCollectibleCollected += OnCollectibleCollected;
    }

    private void OnDisable()
    {
        WinZoneBehaviour.OnWin -= SaveHighscores;
        PlayerCharacter.OnPlayerDeath += _clock.PauseTimer;
        Collectible.OnCollectibleCollected -= OnCollectibleCollected;
    }

    // Save the highscores
    private void SaveHighscores()
    {
        _clock.PauseTimer();
       
        // Check if "BestTime" exists
        if (PlayerPrefs.HasKey("BestTime"))
        {
            _clock.BestTime = Mathf.Min(_clock.CurrentTime, _clock.BestTime);
        }
        else
        {
            _clock.BestTime = _clock.CurrentTime;
        }

        PlayerPrefs.SetFloat("BestTime", _clock.BestTime);
    }

    private void LoadHighscores()
    {
        _clock.BestTime = PlayerPrefs.GetFloat("BestTime");
    }
    public static void RestartGame()
    {
        Collectible._total = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        OnResetProgress?.Invoke();
    }
    public void QuitGame()
    {
        Application.Quit();

        // Stop the editor from playing the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void OnCollectibleCollected() { ++_collectibles; }
}
