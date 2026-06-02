using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/LocationMusicConfig", fileName = "LocationMusicConfig")]
public class LocationMusicConfig : ScriptableObject
{
    public AudioClip[] Light;        // 2 лёгкие
    public AudioClip[] Heavy;        // 2 тяжёлые
    public AudioClip[] Transitions;  // 2 связки
}
