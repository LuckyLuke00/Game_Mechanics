using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clockText = null;

    private bool _isPaused = false;
    private float _time = 0f;
    private float _bestTime = 0f;

    // Getters and Setters
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public float LevelTime { get => _time; set => _time = value; }
    public float BestTime { get => _bestTime; set => _bestTime = value; }

    private void Awake()
    {
        if (_clockText == null)
        {
            Debug.LogError("Clock: Clock text is null!");
        }

        DisplayText();
    }

    private void Update()
    {
        if (_isPaused) return;

        _time += Time.deltaTime;

        DisplayText();
    }

    private void DisplayText()
    {
        _clockText.text = $"{GetMinutes(_time):0}:{GetSeconds(_time):00}";
    }
    
    private int GetMinutes(float time)
    {
        return (int) time / 60;
    }

    private int GetSeconds(float time)
    {
        return (int)time % 60;
    }

    private int GetMilliseconds(float time)
    {
        return (int)(time * 100) % 100;
    }

    private void ResetClock()
    {
        _time = 0f;
    }
}
