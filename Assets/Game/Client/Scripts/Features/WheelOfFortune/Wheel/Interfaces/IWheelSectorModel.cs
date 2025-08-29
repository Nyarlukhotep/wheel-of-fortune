using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public interface IWheelSectorModel
	{
		void SetRewardValue(int value);
		Transform GetRewardSpawnPoint();
	}
}