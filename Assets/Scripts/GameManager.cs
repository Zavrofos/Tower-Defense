using Assets.Scripts.RepPoolObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool IsStartGame = true;
        public int CurrentLevel;

        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private AudioManager _audioManager;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            _objectPooler.Initialize();
            _audioManager.Initialize();
        }
    }
}