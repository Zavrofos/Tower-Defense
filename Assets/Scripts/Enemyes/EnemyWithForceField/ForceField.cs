using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Enemyes.EnemyWithForceField
{
    public class ForceField : MonoBehaviour
    {
        public Transform TransformField;
        public float RadiusToFindTower;
        public Vector3 EnableScale;
        public Vector3 DisableScale;

        private IFinderObjects _finderObjects;
        private IDisposable _finderTowerDisposable;
        private IDisposable _rotatorFieldTransformDisposable;
        private Tween _opelCloseFieldTween;
        
        private bool _moveField;
        private bool _fieldOpened;
        private bool _isDestroyed;
        
        private void Awake()
        {
            _finderObjects = new CircleFinderObjects(RadiusToFindTower);
            
            _finderTowerDisposable = Observable.Interval(TimeSpan.FromSeconds(0.2f))
                .Subscribe((_) => FindTowers())
                .AddTo(this);
        }

        private void FindTowers()
        {
            bool findTowers = _finderObjects.Find("Tower", transform.position).Any();
            
            if (findTowers && !_fieldOpened && !_moveField)
                OpenCloseField(true).Forget();
            
            if(!findTowers && _fieldOpened && !_moveField)
                OpenCloseField(false).Forget();
        }

        private async UniTask OpenCloseField(bool value)
        {
            _moveField = true;
            
            Vector3 endScale = value ? EnableScale  : DisableScale;

            if (value)
            {
                _rotatorFieldTransformDisposable?.Dispose();
                _rotatorFieldTransformDisposable = null;
                _rotatorFieldTransformDisposable = 
                    Observable
                    .EveryUpdate()
                    .Subscribe((_) => TransformField.Rotate(0f, 0f, 30f * Time.deltaTime));
            }
            
            _opelCloseFieldTween?.Kill();
            _opelCloseFieldTween = TransformField
                .DOScale(endScale, 1f)
                .SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            if(_isDestroyed)
                return;

            if (!value)
            {
                _rotatorFieldTransformDisposable?.Dispose();
                _rotatorFieldTransformDisposable = null;
            }
            
            _fieldOpened = value;
            _moveField = false;
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
            _opelCloseFieldTween?.Kill();
            _finderTowerDisposable?.Dispose();
            _finderTowerDisposable = null;
            _rotatorFieldTransformDisposable?.Dispose();
            _rotatorFieldTransformDisposable = null;
        }
    }
}