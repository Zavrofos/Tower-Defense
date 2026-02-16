using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ForceRebuild : MonoBehaviour
{
    private RectTransform _rectTransform;
        
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        Observable.NextFrame()
            .Subscribe((_) => LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform))
            .AddTo(this);
    }
}