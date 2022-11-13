using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance = null;
    [SerializeField] private AudioSource _effectsSource = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
