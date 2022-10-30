using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Clock _clock = null;

    private void Awake()
    {
        _clock = FindObjectOfType<Clock>();

        if (_clock == null)
        {
            Debug.LogError("GameManager: Clock is null!");
        }
        
        LoadHighscores();
    }

    private void OnEnable()
    {
        WinZoneBehaviour.OnWin += SaveHighscores;
    }

    private void OnDisable()
    {
        WinZoneBehaviour.OnWin -= SaveHighscores;
    }

    // Save the highscores
    private void SaveHighscores()
    {
        if (_clock.BestTime == 0f)
        {
            _clock.BestTime = _clock.CurrentTime;
        }
        else
        {
            _clock.BestTime = Mathf.Min(_clock.CurrentTime, _clock.BestTime);
        }

        PlayerPrefs.SetFloat("BestTime", _clock.BestTime);
    }

    private void LoadHighscores()
    {
        _clock.BestTime = PlayerPrefs.GetFloat("BestTime");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
    public void QuitGame()
    {
        Application.Quit();

        // Stop the editor from playing the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
