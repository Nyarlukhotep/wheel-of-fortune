using System;
using System.Collections.Generic;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;

namespace Game.Client.Scripts.Features.WheelOfFortune.Data
{
	[Serializable]
	public class WheelData
	{
		public RewardType RewardType;
		public List<int> SectorValues;
	}
}