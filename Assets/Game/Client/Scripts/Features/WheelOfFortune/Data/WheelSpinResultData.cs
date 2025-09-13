using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Data
{
    public class WheelSpinResultData
    {
        public int WinningSectorId { get; }
        public int WinningValue { get; }
        public Transform SpawnPoint { get; }

        public WheelSpinResultData(int winningSectorId, int winningValue, Transform spawnPoint)
        {
            WinningSectorId = winningSectorId;
            WinningValue = winningValue;
            SpawnPoint = spawnPoint;
        }
    }
}