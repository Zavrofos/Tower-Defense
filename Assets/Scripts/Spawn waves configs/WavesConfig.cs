using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn_waves_configs
{
    [CreateAssetMenu(menuName = "Scriptables/WavesConfig", fileName = "WavesConfig")]
    public class WavesConfig : ScriptableObject
    {
        public List<Wave> Waves;
    }
}