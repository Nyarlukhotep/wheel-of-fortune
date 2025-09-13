using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Data
{
	public struct RewardObjectData
	{
		public Sprite RewardIcon;
		public int RewardValue;

		public RewardObjectData(Sprite rewardIcon, int rewardValue)
		{
			RewardIcon = rewardIcon;
			RewardValue = rewardValue;
		}
	}
}