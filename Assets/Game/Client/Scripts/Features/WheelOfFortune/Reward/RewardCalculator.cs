using System.Collections.Generic;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
    public class RewardCalculator
    {
        private WheelOfFortuneSettings _settings;

        public RewardCalculator(WheelOfFortuneSettings settings)
        {
            _settings = settings;
        }

        public List<RewardDistribution> CalculateDistribution(int totalReward)
        {
            var distribution = new List<RewardDistribution>();
            
            if (totalReward <= _settings.MaxRewardObjectsAmount)
            {
                for (var i = 0; i < totalReward; i++)
                {
                    distribution.Add(new RewardDistribution { Value = 1, Count = 1 });
                }
            }
            else
            {
                var remainingReward = totalReward;
                var remainingObjects = _settings.MaxRewardObjectsAmount;
                
                while (remainingReward > 0 && remainingObjects > 0)
                {
                    var valuePerObject = Mathf.CeilToInt((float)remainingReward / remainingObjects);
                    var objectsWithThisValue = Mathf.Min(remainingObjects, remainingReward / valuePerObject);
                    
                    if (objectsWithThisValue > 0)
                    {
                        distribution.Add(new RewardDistribution 
                        { 
                            Value = valuePerObject, 
                            Count = objectsWithThisValue 
                        });
                        
                        remainingReward -= valuePerObject * objectsWithThisValue;
                        remainingObjects -= objectsWithThisValue;
                    }
                    else
                    {
                        if (distribution.Count > 0)
                        {
                            distribution[^1].Value += remainingReward;
                        }
                        break;
                    }
                }
            }
            
            return distribution;
        }
        
        public List<int> FlattenDistribution(List<RewardDistribution> distribution)
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
