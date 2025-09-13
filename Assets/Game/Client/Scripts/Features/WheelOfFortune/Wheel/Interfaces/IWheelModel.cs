using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelModel
    {
        WheelData CurrentWheelData { get; }
        WheelData LastWheelData { get; }
        WheelSpinResultData CurrentSpinResult { get; }
        
        void SetCurrentWheelData(WheelData wheelData);
        void SetLastWheelData(WheelData wheelData);
        void SetSpinResult(WheelSpinResultData spinResult);
        int GetSectorValue(int sectorId);
    }
}