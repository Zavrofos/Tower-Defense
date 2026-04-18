using UnityEngine;

namespace GameHubDir
{
    public class ShopButtonBlink : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _minScale = 0.9f;
        [SerializeField] private float _maxScale = 1.1f;

        private Vector3 _baseScale;

        private void Awake()
        {
            _baseScale = transform.localScale;

            RectTransform rt = GetComponent<RectTransform>();
            Vector2 size = rt.rect.size;
            Vector2 oldPivot = rt.pivot;
            Vector2 newPivot = new Vector2(0.5f, 0.5f);
            Vector2 delta = (newPivot - oldPivot) * size;
            rt.pivot = newPivot;
            rt.anchoredPosition += delta;
        }

        private void Update()
        {
            float t = (Mathf.Sin(Time.unscaledTime * _speed * Mathf.PI) + 1f) / 2f;
            float scale = Mathf.Lerp(_minScale, _maxScale, t);
            transform.localScale = _baseScale * scale;
        }

        private void OnDisable()
        {
            transform.localScale = _baseScale;
        }
    }
}
