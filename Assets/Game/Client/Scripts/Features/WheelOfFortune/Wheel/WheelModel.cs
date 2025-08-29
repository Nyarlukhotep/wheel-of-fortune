using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelModel : IWheelModel
	{
		private WheelData _currentWheelData;
		private WheelSpinResultData _currentSpinResult;
    
		public WheelData CurrentWheelData => _currentWheelData;

		public WheelSpinResultData CurrentSpinResult => _currentSpinResult;

		public void SetWheelData(WheelData wheelData)
		{
			_currentWheelData = wheelData;
		}
		
		public void SetSpinResult(WheelSpinResultData spinResult)
		{
			_currentSpinResult = spinResult;
		}
    
		public int GetSectorValue(int sectorId)
		{
			if (_currentWheelData != null && sectorId >= 0 && sectorId < _currentWheelData.SectorValues.Count)
			{
				return _currentWheelData.SectorValues[sectorId];
			}
			return 0;
		}
	}
}