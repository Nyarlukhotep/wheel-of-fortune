using System;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelAnimationController
    {
        void StartWheelSpinning(int targetSector, Action onCompletedCallback);
    }
}