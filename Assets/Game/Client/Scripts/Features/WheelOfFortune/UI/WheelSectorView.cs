using Game.Client.Scripts.Features.WheelOfFortune.UI;
using TMPro;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
    public class WheelSectorView : MonoBehaviour, IWheelSectorView
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Transform _rewardSpawnPoint;

        public void SetRewardText(string text)
        {
            _valueText?.SetText(text);
        }

        public Transform GetRewardSpawnPoint()
        {
            return _rewardSpawnPoint;
        }
    }
}
