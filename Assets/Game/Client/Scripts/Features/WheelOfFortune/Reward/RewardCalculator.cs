using System.Collections.Generic;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using UnityEngine;

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
                    distribution.Add(new RewardDistributionData { Value = 1, Count = 1 });
                }
            }
            else
            {
                var baseValue = totalReward / _settings.MaxRewardObjectsAmount;
                var remainder = totalReward % _settings.MaxRewardObjectsAmount;
                
                if (baseValue > 0)
                {
                    distribution.Add(new RewardDistributionData 
                    { 
                        Value = baseValue, 
                        Count = _settings.MaxRewardObjectsAmount - remainder 
                    });
                }
                
                if (remainder > 0)
                {
                    distribution.Add(new RewardDistributionData 
                    { 
                        Value = baseValue + 1, 
                        Count = remainder 
                    });
                }
            }
            
            return distribution;
        }
        
        public List<int> FlattenDistributionData(List<RewardDistributionData> distribution)
        {
            var result = new List<int>();
            
            foreach (var dist in distribution)
            {
                for (var i = 0; i < dist.Count; i++)
                {
                    result.Add(dist.Value);
                }
            }
            
            return result;
        }
    }
}
