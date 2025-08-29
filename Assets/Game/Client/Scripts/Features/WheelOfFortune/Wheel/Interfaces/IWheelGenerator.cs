using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelGenerator
    {
        WheelData GenerateWheel();
        int GetRandomSector();
        void SetLastWonRewardType(RewardType rewardType);
    }
}