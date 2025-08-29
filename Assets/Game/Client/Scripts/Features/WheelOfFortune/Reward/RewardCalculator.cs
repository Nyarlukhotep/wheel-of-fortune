using System.Collections.Generic;
using Game.Client.Scripts.Features.WheelOfFortune.Data;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
    public class RewardCalculator
    {
        private readonly WheelOfFortuneSettings _settings;

        public RewardCalculator(WheelOfFortuneSettings settings)
        {
            _settings = settings;
        }

        public List<RewardDistributionData> CalculateDistribution(int totalReward)
        {
            var distribution = new List<RewardDistributionData>();
            
            if (totalReward <= _settings.MaxRewardObjectsAmount)
            {
                for (var i = 0; i < totalReward; i++)
                {
                    distribution.Add(new RewardDistributionData(1));
                }
            }
            else
            {
                var baseValue = totalReward / _settings.MaxRewardObjectsAmount;
                var remainder = totalReward % _settings.MaxRewardObjectsAmount;
                
                if (baseValue > 0)
                {
                    for (var i = 0; i < _settings.MaxRewardObjectsAmount - remainder; i++)
                    {
                        distribution.Add(new RewardDistributionData(baseValue));
                    }
                }

                if (remainder < 1)
                {
                    return distribution;
                }
                
                for (var i = 0; i < remainder; i++)
                {
                    distribution.Add(new RewardDistributionData(baseValue + 1));
                }
            }
            
            return distribution;
        }
    }
}
