using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Audio;

public class LocationMusicPlayer : MonoBehaviour
{
    [SerializeField] private LocationMusicConfig _config;
    [SerializeField] private AudioMixerGroup _outputGroup; // опционально, для маршрутизации в микшер

    [Header("Thresholds")]
    [SerializeField] private int _highThreshold = 10;
    [SerializeField] private int _lowThreshold = 8;
    [SerializeField] private float _minPlayTime = 5f;

    [Header("Fades")]
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField] private float _fadeOutTime = 3f;         // затухание предыдущего трека
    [SerializeField] private float _fadeInTime = 3f;          // появление следующего трека
    [SerializeField] private float _fadeInLeadBeforeEnd = 2f; // за сколько секунд до конца транзишона стартует следующий

    private enum MusicState { Light, Heavy }

    private MusicState _state;
    private bool _isTransitioning;
    private float _trackStartTime;

    private AudioSource _transitionSource;
    private AudioSource[] _mainSources;
    private Coroutine[] _mainFades;
    private int _activeMain;

    private void Awake()
    {
        _transitionSource = CreateSource();
        _mainSources = new[] { CreateSource(), CreateSource() };
        _mainFades = new Coroutine[2];
        _activeMain = 0;
    }

    private AudioSource CreateSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        source.spatialBlend = 0f;
        source.volume = _volume;
        source.outputAudioMixerGroup = _outputGroup;
        return source;
    }

    private void Start()
    {
        if (_config == null)
            return;

        _state = MusicState.Light;
        PlayMainImmediate(_config.Light);
    }

    private void Update()
    {
        if (_config == null || _isTransitioning)
            return;

        // правило 5 секунд: основной трек должен проиграть минимум _minPlayTime до перехода
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

        AudioSource oldMain = _mainSources[_activeMain];
        int nextIndex = 1 - _activeMain;
        AudioSource newMain = _mainSources[nextIndex];

        // 1. транзишон стартует на полной громкости
        AudioClip transitionClip = GetRandomClip(_config.Transitions);
        float transitionLength = 0f;
        if (transitionClip)
        {
            _transitionSource.loop = false;
            _transitionSource.clip = transitionClip;
            _transitionSource.volume = _volume;
            _transitionSource.Play();
            transitionLength = transitionClip.length;
        }

        // 2. предыдущий трек плавно затухает за _fadeOutTime секунд
        if (oldMain.isPlaying)
        {
            StopMainFade(_activeMain);
            _mainFades[_activeMain] = StartCoroutine(FadeOut(oldMain, _fadeOutTime));
        }

        // 3. ждём до момента "за _fadeInLeadBeforeEnd секунд до конца транзишона"
        float waitBeforeNext = Mathf.Max(0f, transitionLength - _fadeInLeadBeforeEnd);
        yield return WaitUnscaled(waitBeforeNext);

        // 4. следующий трек стартует и плавно появляется за _fadeInTime секунд
        AudioClip nextClip = GetRandomClip(target == MusicState.Heavy ? _config.Heavy : _config.Light);
        if (nextClip)
        {
            // отменяем возможный незавершённый фейд на этом источнике,
            // чтобы старая FadeOut не оборвала и не «передёрнула» свежий трек
            StopMainFade(nextIndex);

            newMain.clip = nextClip;
            newMain.loop = true;
            newMain.volume = 0f;
            newMain.Play();
            _mainFades[nextIndex] = StartCoroutine(FadeIn(newMain, _fadeInTime));
        }

        _state = target;
        _activeMain = nextIndex;
        _trackStartTime = Time.unscaledTime;

        // не разрешаем новый переход, пока следующий трек не появился полностью
        yield return WaitUnscaled(_fadeInTime);

        _isTransitioning = false;
    }

    private void PlayMainImmediate(AudioClip[] clips)
    {
        AudioClip clip = GetRandomClip(clips);
        if (!clip)
            return;

        StopMainFade(_activeMain);

        AudioSource source = _mainSources[_activeMain];
        source.clip = clip;
        source.loop = true;
        source.volume = _volume;
        source.Play();
        _trackStartTime = Time.unscaledTime;
    }

    private void StopMainFade(int index)
    {
        if (_mainFades[index] == null)
            return;

        StopCoroutine(_mainFades[index]);
        _mainFades[index] = null;
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        if (duration <= 0f)
        {
            source.volume = 0f;
            source.Stop();
            yield break;
        }

        float startVolume = source.volume;
        float t = 0f;

        while (t < duration && source.isPlaying)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
    }

    private IEnumerator FadeIn(AudioSource source, float duration)
    {
        if (duration <= 0f)
        {
            source.volume = _volume;
            yield break;
        }

        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(0f, _volume, t / duration);
            yield return null;
        }

        source.volume = _volume;
    }

    private IEnumerator WaitUnscaled(float seconds)
    {
        if (seconds <= 0f)
            yield break;

        float t = 0f;

        while (t < seconds)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }

    private int GetEnemiesCount()
    {
        if (GameManager.Instance == null)
            return 0;

        GameManagerInGame level = GameManager.Instance.CurrentGameManagerLevel;
        return level != null ? level.CurrentEnemies.Count : 0;
    }
}
