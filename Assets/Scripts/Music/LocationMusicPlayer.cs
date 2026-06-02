using System.Collections;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocationMusicPlayer : MonoBehaviour
{
    [SerializeField] private LocationMusicConfig _config;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private int _highThreshold = 10;
    [SerializeField] private int _lowThreshold = 8;
    [SerializeField] private float _minPlayTime = 5f;

    private enum MusicState { Light, Heavy }

    private MusicState _state;
    private bool _isTransitioning;
    private float _trackStartTime;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_config == null)
            return;

        _state = MusicState.Light;
        PlayMainTrack(_config.Light);
    }

    private void Update()
    {
        if (_config == null || _isTransitioning)
            return;

        // правило 5 секунд: основной трек должен проиграть минимум _minPlayTime,
        // прежде чем разрешён переход (время реальное, т.к. музыка играет и на паузе)
        if (Time.unscaledTime - _trackStartTime < _minPlayTime)
            return;

        int enemiesCount = GetEnemiesCount();

        if (_state == MusicState.Light && enemiesCount > _highThreshold)
            StartCoroutine(TransitionTo(MusicState.Heavy));
        else if (_state == MusicState.Heavy && enemiesCount < _lowThreshold)
            StartCoroutine(TransitionTo(MusicState.Light));
    }

    private IEnumerator TransitionTo(MusicState target)
    {
        _isTransitioning = true;

        AudioClip transitionClip = GetRandomClip(_config.Transitions);
        if (transitionClip)
        {
            _audioSource.loop = false;
            _audioSource.clip = transitionClip;
            _audioSource.Play();

            // ждём, пока связка доиграет до конца (yield return null тикает и на паузе)
            while (_audioSource.isPlaying)
                yield return null;
        }

        _state = target;
        PlayMainTrack(target == MusicState.Heavy ? _config.Heavy : _config.Light);

        _isTransitioning = false;
    }

    private void PlayMainTrack(AudioClip[] clips)
    {
        AudioClip clip = GetRandomClip(clips);
        if (!clip)
            return;

        _audioSource.loop = true;
        _audioSource.clip = clip;
        _audioSource.Play();
        _trackStartTime = Time.unscaledTime;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }

    private int GetEnemiesCount()
    {
        GameManagerInGame level = GameManager.Instance.CurrentGameManagerLevel;
        return level != null ? level.CurrentEnemies.Count : 0;
    }
}
