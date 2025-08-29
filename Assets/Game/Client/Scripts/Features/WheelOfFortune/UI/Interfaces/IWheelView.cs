using System;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
    public interface IWheelView
    {
        event Action OnSpinButtonPressed;
        
        Transform Transform { get; }
        Transform WheelContainer { get; }
        IWheelSectorView[] WheelSectors { get; }
        
        void Initialize();
        void SetTitle(string title);
        void SetButtonText(string text);
        void SetButtonTextColor(Color color);
        void SetButtonInteractable(bool interactable);
        void SetRewardText(string text);
        void ShowRewardText();
        void HideRewardText();
        void UpdateRewardIcon(Sprite sprite);
        void ShowRewardIcon();
        void HideRewardIcon();
    }
}