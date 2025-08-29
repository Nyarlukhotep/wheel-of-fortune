using System;
using System.Collections.Generic;
using System.Linq;
using Game.Client.Scripts.Constants;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;
using Game.Client.Scripts.Utils.Extensions;
using Random = System.Random;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public class WheelGenerator
    {
        private readonly WheelOfFortuneSettings _settings;
        private readonly Random _random;
        
        private RewardType _lastGeneratedType;
        private int _sectorCount;

        public WheelGenerator(WheelOfFortuneSettings settings)
        {
            _settings = settings;
            
            _random = new Random();
            _sectorCount = GameConstants.WheelOfFortune.SECTORS_COUNT;
        }
        
        public WheelData GenerateWheel()
        {
            var rewardType = GenerateRewardType();
            _lastGeneratedType = rewardType;
            
            var slotValues = GenerateSlotValues();
            
            return new WheelData
            {
                RewardType = rewardType,
                SectorValues = slotValues
            };
        }
        
        private RewardType GenerateRewardType()
        {
            var availableTypes = Enum.GetValues(typeof(RewardType))
                .Cast<RewardType>()
                .Where(type => type != _lastGeneratedType)
                .ToArray();

            if (availableTypes.Length == 0)
            {
                availableTypes = Enum.GetValues(typeof(RewardType)).Cast<RewardType>().ToArray();
            }
            
            return availableTypes[_random.Next(availableTypes.Length)];
        }
        
        private List<int> GenerateSlotValues()
        {
            var possibleValues = new List<int>();
            
            for (var i = _settings.MinRewardAmount; i <= _settings.MaxRewardAmount; i += _settings.RewardValueStep)
            {
                possibleValues.Add(i);
            }

            possibleValues = possibleValues.Shuffle();

            return _sectorCount > possibleValues.Count 
                ? possibleValues 
                : possibleValues.GetRange(0, _sectorCount);
        }
        
        public int GetRandomSector()
        {
            return _random.Next(_sectorCount);
        }
    }
}
