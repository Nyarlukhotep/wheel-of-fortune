using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelSectorModel
	{
		private readonly IWheelSectorView _view;

		public WheelSectorModel(IWheelSectorView view)
		{
			_view = view;
		}

		public void SetRewardValue(int value)
		{
			_view.SetRewardText($"{value}");
		}

		public Transform GetRewardSpawnPoint()
		{
			return _view.GetRewardSpawnPoint();
		}
	}
}