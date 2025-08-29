using Game.Client.Scripts.Features.WheelOfFortune.Data;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelSpinService : IWheelSpinService
	{
		private readonly IWheelGenerator _wheelGenerator;
		private readonly IWheelModel _wheelModel;

		public WheelSpinService(IWheelGenerator wheelGenerator, IWheelModel wheelModel)
		{
			_wheelGenerator = wheelGenerator;
			_wheelModel = wheelModel;
		}

		public void DetermineSpinResult(IWheelController controller)
		{
			var winningSectorId = _wheelGenerator.GetRandomSector();
			var winningValue = _wheelModel.GetSectorValue(winningSectorId);
			var spawnPoint = controller.GetWheelSectorById(winningSectorId)?.GetRewardSpawnPoint();

			_wheelModel.SetSpinResult(new WheelSpinResultData(winningSectorId, winningValue, spawnPoint));
			
			Debug.Log($"Выигрышный слот: {winningSectorId}, Значение: {winningValue}");
		}
    
		public void GenerateNewWheel()
		{
			var newWheelData = _wheelGenerator.GenerateWheel();
			_wheelModel.SetWheelData(newWheelData);
		}
	}
}