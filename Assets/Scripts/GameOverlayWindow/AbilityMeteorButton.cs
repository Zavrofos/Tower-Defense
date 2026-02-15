using System;
using System.Threading;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using Assets.Scripts.MeteorsAbility;
using Assets.Scripts.RepPoolObject;
using Cysharp.Threading.Tasks;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameOverlayWindow
{
    public class AbilityMeteorButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _backgroundCount;
        [SerializeField] private TMP_Text _count;

        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;
        
        private CancellationTokenSource _cancellationTokenSourceMeteors = new();
        
        private void Awake()
        {
            _button.onClick.AddListener(PlayMeteors);
        }

        private void PlayMeteors()
        {
            SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count--;
            _count.text = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count.ToString();
            
            if (SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count == 0)
                SetInteractableButton(false);

            PlayMeteorShower().Forget();
        }

        public void SetInteractableButton(bool value)
        {
            _button.interactable = value;
            _icon.color = value ? _enableColor : _disableColor;
            _backgroundCount.color = value ? _enableColor : _disableColor;
        }

        public void SetCountText(string count)
        {
            _count.text = count;
        }
        
        private async UniTask PlayMeteorShower()
        {
            _cancellationTokenSourceMeteors?.Cancel();
            _cancellationTokenSourceMeteors?.Dispose();
            _cancellationTokenSourceMeteors = new CancellationTokenSource();
            
            SetInteractableButton(false);

            var tasks = new UniTask[GameManager.Instance.CurrentGameManagerLevel.PointsForMeteors.Length];

            for (int i = 0; i < GameManager.Instance.CurrentGameManagerLevel.PointsForMeteors.Length; i++)
            {
                Vector3 pos = GameManager.Instance.CurrentGameManagerLevel.PointsForMeteors[i].position;
                tasks[i] = SpawnMeteorsAtPoint(pos, _cancellationTokenSourceMeteors.Token);
            }

            await UniTask.WhenAll(tasks);
        
            if(_cancellationTokenSourceMeteors.Token.IsCancellationRequested)
                return;
        
            SetInteractableButton(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count > 0);
        }
        
        private async UniTask SpawnMeteorsAtPoint(Vector3 position, CancellationToken token)
        {
            const int meteorsCount = 3;

            for (int i = 0; i < meteorsCount; i++)
            {
                float delay = Random.Range(1f, 3f);
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
                if(token.IsCancellationRequested)
                    return;

                Meteor meteor = (Meteor)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.Meteor, position, Quaternion.identity);
                meteor.PlayMeteorsShower(token).Forget();
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            _cancellationTokenSourceMeteors?.Cancel();
            _cancellationTokenSourceMeteors?.Dispose();
        }
    }
}