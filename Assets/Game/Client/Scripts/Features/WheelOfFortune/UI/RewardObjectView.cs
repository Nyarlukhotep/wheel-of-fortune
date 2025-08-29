using Game.Client.Scripts.Core.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
    public class RewardObjectView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image _rewardIcon;
        
        public void SetIcon(Sprite sprite)
        {
            _rewardIcon.sprite = sprite;
        }

        public void Reset()
        {
            _rewardIcon.sprite = null;
        }
    }
}