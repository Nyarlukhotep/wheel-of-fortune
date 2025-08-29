using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelModel
    {
        WheelData CurrentWheelData { get; }
        WheelSpinResultData CurrentSpinResult { get; }
        
        void SetWheelData(WheelData wheelData);
        void SetSpinResult(WheelSpinResultData spinResult);
        int GetSectorValue(int sectorId);
    }
}