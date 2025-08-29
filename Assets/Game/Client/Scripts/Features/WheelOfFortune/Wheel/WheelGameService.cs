using Game.Client.Scripts.Features.WheelOfFortune.Data;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelGameService
	{
		private readonly WheelGenerator _wheelGenerator;
		private readonly WheelModel _wheelModel;

		public WheelGameService(WheelGenerator wheelGenerator, WheelModel wheelModel)
		{
			_wheelGenerator = wheelGenerator;
			_wheelModel = wheelModel;
		}

		public void DetermineSpinResult(WheelController controller)
		{
			var winningSectorId = _wheelGenerator.GetRandomSector();
			var winningValue = _wheelModel.GetSectorValue(winningSectorId);
			var spawnPoint = controller.GetWheelSectorById(winningSectorId)?.GetRewardSpawnPoint();

			_wheelModel.SetSpinResult(new WheelSpinResult(winningSectorId, winningValue, spawnPoint));
			
			Debug.Log($"Выигрышный слот: {winningSectorId}, Значение: {winningValue}");
		}
    
		public void GenerateNewWheel()
		{
			var newWheelData = _wheelGenerator.GenerateWheel();
			_wheelModel.SetWheelData(newWheelData);
		}
	}
}