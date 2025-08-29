using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
    public class CooldownState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IWheelController _controller;
        private readonly IWheelGenerator _wheelGenerator;
        private readonly IWheelModel _wheelModel;
        private readonly WheelOfFortuneSettings _settings;
        private readonly CancellationToken _cancellationToken;

        private float _currentTime;

        public CooldownState(
            IStateMachine stateMachine,
            IWheelController controller,
            IWheelGenerator wheelGenerator,
            IWheelModel wheelModel,
            WheelOfFortuneSettings settings,
            CancellationToken cancellationToken)
        {
            _wheelModel = wheelModel;
            _cancellationToken = cancellationToken;
            _wheelGenerator = wheelGenerator;
            _stateMachine = stateMachine;
            _controller = controller;
            _settings = settings;
        }

        public void Enter()
        {
            _currentTime = _settings.SpinCooldownDuration;
        
            _controller.SetButtonInteractable(false);
            _controller.SetButtonText($"{_settings.SpinCooldownDuration}");
            _controller.SetButtonTextColor(_settings.CooldownButtonTextColor);
            _controller.HideRewardText();
            _controller.ShowRewardIcon();
        
            GenerateWheelEverySecond();
        }

        public void Exit()
        {
            _wheelModel.SetLastWheelData(_wheelModel.CurrentWheelData);
            _controller?.SetButtonTextColor(_settings.DefaultButtonTextColor);
        }

        private async Task GenerateWheelEverySecond()
        {
            try
            {
                while (_currentTime > 0)
                {
                    UpdateWheel();

                    _controller.UpdateCooldownCounter(_currentTime);
                    _controller.DisplayCurrentWheel();

                    await Task.Delay(1000, _cancellationToken);

                    _currentTime -= 1f;
                }

                if (_controller != null)
                {
                    _stateMachine.Enter<ActiveState>();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void UpdateWheel()
        {
            if (_currentTime == 1)
            {
                if (_wheelModel.LastWheelData != null)
                {
                    _wheelGenerator.SetLastWonRewardType(_wheelModel.LastWheelData.RewardType);
                }
            }
                    
            var wheelData = _wheelGenerator.GenerateWheel();
            _wheelModel.SetCurrentWheelData(wheelData);
        }
    }
}
