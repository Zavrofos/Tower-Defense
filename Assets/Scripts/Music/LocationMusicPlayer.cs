using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Audio;

public class LocationMusicPlayer : MonoBehaviour
{
    public static LocationMusicPlayer Instance;

    [SerializeField] private LocationMusicConfig _config;
    [SerializeField] private AudioMixerGroup _outputGroup; // группа музыкального микшера (берётся из источника в сцене)

    [Header("Thresholds")]
    [SerializeField] private int _highThreshold = 10;
    [SerializeField] private int _lowThreshold = 8;
    [SerializeField] private float _minPlayTime = 5f;

    [Header("Fades")]
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField] private float _fadeOutTime = 3f;         // затухание предыдущего трека
    [SerializeField] private float _fadeInTime = 3f;          // появление следующего трека
    [SerializeField] private float _fadeInLeadBeforeEnd = 2f; // за сколько секунд до конца транзишона стартует следующий
    [SerializeField] private float _transitionFadeTime = 1f;  // появление/затухание самого транзишона

    private enum MusicState { Light, Heavy }

    private MusicState _state;
    private bool _isTransitioning;
    private float _trackStartTime;

    private AudioSource _transitionSource;
    private AudioSource[] _mainSources;
    private Coroutine[] _mainFades;
    private Coroutine _transitionEnvelope;
    private int _activeMain;
    private bool _isGamePaused;

    private void Awake()
    {
        Instance = this;

        // источник, уже стоящий в сцене, направлен в группу музыкального микшера —
        // берём его маршрутизацию, чтобы создаваемые источники шли в тот же микшер
        AudioSource sceneSource = GetComponent<AudioSource>();
        if (sceneSource != null && sceneSource.outputAudioMixerGroup != null)
            _outputGroup = sceneSource.outputAudioMixerGroup;

        _transitionSource = CreateSource();
        _mainSources = new[] { CreateSource(), CreateSource() };
        _mainFades = new Coroutine[2];
        _activeMain = 0;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
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
        HandlePause();

        if (_config == null || _isTransitioning || _isGamePaused)
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

    // пауза игры (Time.timeScale == 0) ставит на паузу только музыку локации; эмбиенс не трогаем
    private void HandlePause()
    {
        if (Time.timeScale == 0f && !_isGamePaused)
        {
            _isGamePaused = true;

            _transitionSource.Pause();
            foreach (var source in _mainSources)
                source.Pause();
        }
        else if (Time.timeScale > 0f && _isGamePaused)
        {
            _isGamePaused = false;

            _transitionSource.UnPause();
            foreach (var source in _mainSources)
                source.UnPause();
        }
    }

    // выключает музыку локации и эмбиенс (при победе/проигрыше, чтобы джингл звучал чисто).
    // эмбиенс сидит в той же группе музыкального микшера, поэтому глушим всю группу
    public void StopMusic()
    {
        StopAllCoroutines();
        _isTransitioning = false;
        _isGamePaused = false;
        _transitionEnvelope = null;

        for (int i = 0; i < _mainFades.Length; i++)
            _mainFades[i] = null;

        _transitionSource.Stop();
        foreach (var source in _mainSources)
            source.Stop();

        if (_outputGroup == null)
            return;

        foreach (var source in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (source.outputAudioMixerGroup == _outputGroup)
                source.Stop();
        }
    }

    private IEnumerator TransitionTo(MusicState target)
    {
        _isTransitioning = true;

        AudioSource oldMain = _mainSources[_activeMain];
        int nextIndex = 1 - _activeMain;
        AudioSource newMain = _mainSources[nextIndex];

        // 1. транзишон: плавное появление в начале и плавное затухание к концу клипа
        AudioClip transitionClip = GetRandomClip(_config.Transitions);
        float transitionLength = 0f;
        if (transitionClip)
        {
            transitionLength = transitionClip.length;

            if (_transitionEnvelope != null)
                StopCoroutine(_transitionEnvelope);

            _transitionEnvelope = StartCoroutine(PlayTransition(transitionClip, transitionLength));
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

    private IEnumerator PlayTransition(AudioClip clip, float length)
    {
        // ограничиваем длительность фейдов половиной клипа, чтобы появление и
        // затухание не пересекались на коротких транзишонах
        float fade = Mathf.Min(_transitionFadeTime, length * 0.5f);

        _transitionSource.loop = false;
        _transitionSource.clip = clip;
        _transitionSource.volume = 0f;
        _transitionSource.Play();

        // плавное появление
        yield return FadeIn(_transitionSource, fade);

        // держим на полной громкости до момента затухания
        yield return WaitUnscaled(length - fade * 2f);

        // плавное затухание к концу транзишона
        yield return FadeOut(_transitionSource, fade);
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

        // на паузе источник приостановлен (isPlaying == false), поэтому держим фейд
        // тоже на паузе, иначе трек оборвётся раньше времени
        while (t < duration && (source.isPlaying || _isGamePaused))
        {
            if (_isGamePaused)
            {
                yield return null;
                continue;
            }

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
            if (_isGamePaused)
            {
                yield return null;
                continue;
            }

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
            if (_isGamePaused)
            {
                yield return null;
                continue;
            }

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
