using System;
using System.Linq;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
    public class WheelController : IWheelController
    {
        private readonly IWheelAnimationController _animationController;
        private readonly IWheelView _view;
        private readonly IWheelModel _model;
        private readonly WheelOfFortuneSettings _settings;

        private WheelSectorModel[] _wheelSectorsData;
        private WheelData _currentWheelData;

        public event Action OnSpinButtonPressed;

        public WheelController(IWheelAnimationController animationController,
            IWheelModel model,
            IWheelView view,
            WheelOfFortuneSettings settings)
        {
            _model = model;
            _animationController = animationController;
            _view = view;
            _settings = settings;
        }
        
        public void Initialize()
        {
            InitializeView();
            CreateSectorsData();
        }

        public void Dispose()
        {
            OnSpinButtonPressed = null;
            
            if (_view != null)
            {
                _view.OnSpinButtonPressed -= SpinButtonPressed;
            }
        }
        
        public void SetButtonInteractable(bool interactable)
        {
            _view.SetButtonInteractable(interactable);
        }
        
        public void DisplayCurrentWheel()
        {
            if (_model.CurrentWheelData != null)
            {
                DisplayWheel(_model.CurrentWheelData);
            }
        }
        
        public void SetButtonTextColor(Color color)
        {
            _view.SetButtonTextColor(color);
        }
        
        public void SetButtonText(string text)
        {
            _view.SetButtonText(text);
        }

        public void UpdateCooldownCounter(float currentTime)
        {
            _view.SetButtonText($"{currentTime}");
        }

        public void SetRewardText(string text)
        {
            _view.SetRewardText(text);
        }

        public void ShowRewardText()
        {
            _view.ShowRewardText();
        }
        
        public void HideRewardText()
        {
            _view.HideRewardText();
        }

        public void ShowRewardIcon()
        {
            _view.ShowRewardIcon();
        }

        public void HideRewardIcon()
        {
            _view.HideRewardIcon();
        }

        public IWheelSectorModel GetWheelSectorById(int sectorId)
        {
            if (sectorId > _wheelSectorsData.Length - 1)
            {
                Debug.LogError($"[ERROR] Sector with id {sectorId} not exist");
                return null;
            }

            return _wheelSectorsData[sectorId];
        }
        

#region private

        private void InitializeView()
        {
            _view.SetTitle(_settings.WheelTitle);
            _view.SetButtonText(_settings.SpinButtonTitle);
            
            _view.OnSpinButtonPressed += SpinButtonPressed;
        }

        private void CreateSectorsData()
        {
            _wheelSectorsData = new WheelSectorModel[_view.WheelSectors.Length];
            
            for (var i = 0; i < _wheelSectorsData.Length; i++)
            {
                _wheelSectorsData[i] = new WheelSectorModel(_view.WheelSectors[i]);
            }
        }
        
        private void SpinButtonPressed()
        {
            StartSpin();
        }

        private void StartSpin()
        {
            OnSpinButtonPressed?.Invoke();
            
            _animationController.StartWheelSpinning(_model.CurrentSpinResult.WinningSectorId, OnCompletedSpinningCallback);
        }

        private void OnCompletedSpinningCallback()
        {
        }

        private void DisplayWheel(WheelData wheelData)
        {
            UpdateSectors(wheelData);
            UpdateRewardIcon(wheelData.RewardType);
        }

        private void UpdateSectors(WheelData wheelData)
        {
            for (var i = 0; i < _wheelSectorsData.Length && i < wheelData.SectorValues.Count; i++)
            {
                _wheelSectorsData[i].SetRewardValue(wheelData.SectorValues[i]);
            }
        }

        private void UpdateRewardIcon(RewardType rewardType)
        {
            var visualData = _settings.RewardVisuals.FirstOrDefault(x => x.RewardType == rewardType);
            
            if (visualData == null)
            {
                Debug.LogError($"[ERROR] Visual data for reward {rewardType} not found");
                return;
            }
            
            _view.UpdateRewardIcon(visualData.RewardSprite);
        }

#endregion
    }
}
