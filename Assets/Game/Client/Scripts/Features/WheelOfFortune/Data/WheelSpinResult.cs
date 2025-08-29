using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Data
{
    public class WheelSpinResult
    {
        public int WinningSectorId { get; }
        public int WinningValue { get; }
        public Transform SpawnPoint { get; }

        public WheelSpinResult(int winningSectorId, int winningValue, Transform spawnPoint)
        {
            WinningSectorId = winningSectorId;
            WinningValue = winningValue;
            SpawnPoint = spawnPoint;
        }
    }
}