using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject _levelButtonPrefab = null;
    
    private TextMeshProUGUI _levelNumberText = null;
    private TextMeshProUGUI _levelTimeText = null;

    // List of all the level buttons
    private List<int> _levelIndexes = new List<int>();
    private List<string> _levelNames = new List<string>();

    //private int[] _levelIndexes = null;
    //private string[] _levelNames = null;

    private void Awake()
    {
        if (_levelButtonPrefab == null)
        {
            Debug.LogError("LevelSelector: _levelButtonPrefab is null!");
            return;
        }

        StoreLevelData();
        CreateButtons();
    }

    private void StoreLevelData()
    {
        // Get the number of scenes in the build settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        // Loop through the scenes in the build settings
        for (int i = 0; i < sceneCount; i++)
        {
            // Get the scene path
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            // Get the scene name
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // Check if the scene name contains "Level"
            if (sceneName.Contains("Level"))
            {
                // Get the level index
                int levelIndex = int.Parse(sceneName.Substring(5));

                // Add the level index to the array
                _levelIndexes.Add(levelIndex);
                _levelNames.Add(sceneName);
            }
        }
    }

    private void CreateButtons()
    {
        // For every level
        // Create a button inside the child Content game object

        // Get the Content child game object
        Transform content = transform.Find("ScrollRect").Find("Content");

        if (content == null)
        {
            Debug.LogError("LevelSelector: Content is null!");
            return;
        }

        // Loop through the level indexes
        for (int i = 0; i < _levelIndexes.Count; i++)
        {
            // Create a button
            GameObject levelButton = Instantiate(_levelButtonPrefab, content);

            // Get the child text components
            _levelNumberText = levelButton.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            if (!_levelNumberText) Debug.LogError("LevelSelector: _levelNumberText is null!");
            
            _levelTimeText = levelButton.transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
            if (!_levelTimeText) Debug.LogError("LevelSelector: _levelTimeText is null!");

            // Set the text
            _levelNumberText.text = $"{i + 1}";

            string bestTime = GameManager.GetPref(PrefNames.BestTime, _levelNames[i]);

            // If bestTime is an empty string, it means the player has not completed the level yet

            if (bestTime == string.Empty)
            {
                bestTime = "NOT COMPLETED";
            }
            else
            {
                bestTime = Clock.GetTimeText(PlayerPrefs.GetFloat(bestTime));
            }
            _levelTimeText.text = bestTime;
        }

    }
}
