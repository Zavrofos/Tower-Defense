using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class GameOverlay : MonoBehaviour
    { 
        [field: SerializeField] public Button PauseGameButton { get; private set; }
        [field: SerializeField] public Button SetGameFasterButton { get; private set; }
        [field: SerializeField] public Button SetNextWaveButton { get; private set; }
        
        [field: SerializeField] public HealthBar HealthBar { get; private set; }
        [field: SerializeField] public TMP_Text CoinsText { get; private set; }
        [field: SerializeField] public AbilityRocketButton AbilityRocketButton { get; private set; }
        [field: SerializeField] public AbilityMineButton AbilityMineButton { get; private set; }
        [field: SerializeField] public AbilityMeteorButton AbilityMeteorButton { get; private set; }
        [field: SerializeField] public AbilityFoodButton AbilityFoodButton { get; private set; }
        [field: SerializeField] public AbilityPocketMoneyButton AbilityPocketMoneyButton { get; private set; }
        
    }
}