using System;
using TMPro;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Transform _wheelContainer;
        [SerializeField] private WheelRewardView _rewardView;
        [SerializeField] private SpinButton _spinButton;
        [SerializeField] private WheelSectorView[] _wheelSectors;
        
        public Transform WheelContainer => _wheelContainer;

        public WheelSectorView[] WheelSectors => _wheelSectors;

        public event Action OnSpinButtonPressed;

        public void Initialize()
        {
            _spinButton.SetCallback(SpinButtonPressed);
        }

        public void SetButtonInteractable(bool interactable)
        {
            _spinButton?.SetInteractable(interactable);
        }

        public void SetTitle(string text)
        {
            _titleText?.SetText(text);
        }

        public void SetButtonText(string text)
        {
            _spinButton?.SetText(text);
        }

        public void SetButtonTextColor(Color color)
        {
            _spinButton.SetButtonTextColor(color);
        }

        public void UpdateRewardIcon(Sprite rewardSprite)
        {
            _rewardView.SetRewardIcon(rewardSprite);
        }

        public void SetRewardText(string text)
        {
            _rewardView?.SetRewardText(text);
        }

        public void ShowRewardText()
        {
            _rewardView.SetRewardText("0");
            _rewardView.ShowText();
        }
        
        public void HideRewardText()
        {
            _rewardView.HideText();
        }

        public void ShowRewardIcon()
        {
            _rewardView.ShowIcon();
        }

        public void HideRewardIcon()
        {
            _rewardView.HideIcon();
        }

        private void SpinButtonPressed()
        {
            OnSpinButtonPressed?.Invoke();
        }

        private void OnDestroy()
        {
            OnSpinButtonPressed = null;
        }
    }
}
