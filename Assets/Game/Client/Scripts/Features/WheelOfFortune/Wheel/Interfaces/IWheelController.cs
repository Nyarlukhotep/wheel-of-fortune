using System;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public interface IWheelController : IDisposable
    {
        event Action OnSpinButtonPressed;
        
        void Initialize();
        void SetButtonInteractable(bool interactable);
        void SetButtonText(string text);
        void SetButtonTextColor(Color color);
        void DisplayCurrentWheel();
        void UpdateCooldownCounter(float currentTime);
        void SetRewardText(string text);
        void ShowRewardText();
        void HideRewardText();
        void ShowRewardIcon();
        void HideRewardIcon();
        IWheelSectorModel GetWheelSectorById(int sectorId);
    }
}