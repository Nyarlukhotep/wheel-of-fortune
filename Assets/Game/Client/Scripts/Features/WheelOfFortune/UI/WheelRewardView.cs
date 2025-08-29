using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
	public class WheelRewardView : MonoBehaviour
	{
		[SerializeField] private Image _rewardIcon;
		[SerializeField] private TextMeshProUGUI _rewardText;
        
		public void SetRewardText(string text)
		{
			_rewardText?.SetText(text);
		}
		
		public void SetRewardIcon(Sprite sprite)
		{
			_rewardIcon.sprite = sprite;
		}

		public void ShowIcon()
		{
			_rewardIcon.enabled = true;
		}

		public void HideIcon()
		{
			_rewardIcon.enabled = false;
		}

		public void ShowText()
		{
			_rewardText.enabled = true;
		}

		public void HideText()
		{
			_rewardText.enabled = false;
		}
	}
}