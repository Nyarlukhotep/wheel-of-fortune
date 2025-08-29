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
			if (_rewardIcon != null)
			{
				_rewardIcon.sprite = sprite;
			}
		}

		public void ShowIcon()
		{
			if (_rewardIcon != null)
			{
				_rewardIcon.enabled = true;
			}
		}

		public void HideIcon()
		{
			if (_rewardIcon != null)
			{
				_rewardIcon.enabled = false;
			}
		}

		public void ShowText()
		{
			if (_rewardText != null)
			{
				_rewardText.enabled = true;
			}
		}

		public void HideText()
		{
			if (_rewardText != null)
			{
				_rewardText.enabled = false;
			}
		}
	}
}