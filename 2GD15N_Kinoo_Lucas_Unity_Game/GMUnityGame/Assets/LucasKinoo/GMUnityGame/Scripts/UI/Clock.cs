using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clockText = null;

    private bool _isPaused = false;
    private float _time = 0f;
    private int _minutes = 0;
    private int _seconds = 0;
    private int _milliseconds = 0;

    private void Awake()
    {
        if (_clockText == null)
        {
            Debug.LogError("Clock: Clock text is null!");
        }

        DisplayText();
        StartClock();
    }

    private void Update()
    {
        if (_isPaused) return;

        _time += Time.deltaTime;
        _minutes = (int)_time / 60;
        _seconds = (int)_time % 60;
        _milliseconds = (int)(_time * 100) % 100;

        DisplayText();
    }

    private void DisplayText()
    {
        _clockText.text = $"{_minutes:0}:{_seconds:00}";
    }

    private void StartClock()
    {
        _isPaused = false;
    }

    private void StopClock()
    {
        _isPaused = true;
    }

    private void ResetClock()
    {
        _time = 0f;
        _minutes = 0;
        _seconds = 0;
        _milliseconds = 0;
    }
}
