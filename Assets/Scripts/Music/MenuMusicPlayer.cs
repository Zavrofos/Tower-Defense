using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuMusicPlayer : MonoBehaviour
{
    public static MenuMusicPlayer Instance;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        // singleton + переживает загрузку сцен (меню -> хаб -> меню)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }

    public void PlayMusic()
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
}
