using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
	public interface IWheelSectorView
	{
		void SetRewardText(string text);
		Transform GetRewardSpawnPoint();
	}
}