using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Clock _clock = null;
    private int _bestTime = 0;

    private void Awake()
    {
        _clock = FindObjectOfType<Clock>();
        
        if (_clock == null)
        {
            Debug.LogError("GameManager: Clock is null!");
        }
    }

    private void OnEnable()
    {
        PlayerCharacter.OnPlayerDeath += SaveHighscores;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnPlayerDeath -= SaveHighscores;
    }

    // Save the highscores
    private void SaveHighscores()
    {
        PlayerPrefs.SetInt("BestTime", _bestTime);
    }
}
