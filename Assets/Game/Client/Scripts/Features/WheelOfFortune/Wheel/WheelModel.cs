using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelModel
	{
		private WheelData _currentWheelData;
		private WheelSpinResult _currentSpinResult;
    
		public WheelData CurrentWheelData => _currentWheelData;

		public WheelSpinResult CurrentSpinResult => _currentSpinResult;

		public void SetWheelData(WheelData wheelData)
		{
			_currentWheelData = wheelData;
		}
		
		public void SetSpinResult(WheelSpinResult spinResult)
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