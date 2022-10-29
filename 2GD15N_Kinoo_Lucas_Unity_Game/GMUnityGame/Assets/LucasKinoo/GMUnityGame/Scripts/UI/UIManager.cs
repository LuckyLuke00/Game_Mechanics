using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMenu = null;

    private void OnEnable()
    {
        PlayerCharacter.OnPlayerDeath += ShowGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnPlayerDeath -= ShowGameOverMenu;
    }

    public void ShowGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
