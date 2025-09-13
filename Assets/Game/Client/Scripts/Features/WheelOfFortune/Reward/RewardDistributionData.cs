using System;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
	[Serializable]
	public class RewardDistributionData
	{
		public int Value { get; }

		public RewardDistributionData(int value)
		{
			Value = value;
		}
	}
}