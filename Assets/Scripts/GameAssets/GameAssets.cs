using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton
    public static GameAssets Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion
    public SoundAudioClip[] SoundAudioClips;
}
