using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelGenerator
    {
        WheelData GenerateWheel();
        int GetRandomSector();
    }
}