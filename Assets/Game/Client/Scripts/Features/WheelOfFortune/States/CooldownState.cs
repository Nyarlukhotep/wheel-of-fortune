using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
    public class CooldownState : IState, IDisposable
    {
        private readonly WheelController _controller;
        private readonly WheelOfFortuneSettings _settings;
        private readonly WheelGenerator _wheelGenerator;
        private readonly IStateMachine _stateMachine;

        private CancellationTokenSource _cancellationTokenSource;
        private float _currentTime;
        private readonly WheelGameService _wheelGameService;

        public CooldownState(IStateMachine stateMachine, WheelController controller, WheelGameService wheelGameService, WheelOfFortuneSettings settings)
        {
            _wheelGameService = wheelGameService;
            _stateMachine = stateMachine;
            _controller = controller;
            _settings = settings;
        }

        public void Dispose()
        {
            _currentTime = 0;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        public async void Enter()
        {
            try
            {
                _currentTime = _settings.SpinCooldownDuration;
            
                _controller.SetButtonInteractable(false);
                _controller.SetButtonText($"{_settings.SpinCooldownDuration}");
                _controller.SetButtonTextColor(_settings.CooldownButtonTextColor);
                _controller.HideRewardText();
                _controller.ShowRewardIcon();
            
                await GenerateWheelEverySecond();
            }
            catch (Exception e)
            {
                Debug.LogError($"[ERROR] Enter {typeof(CooldownState)}: {e.Message} | {e.StackTrace}");
            }
        }

        public void Exit()
        {
            _controller.SetButtonTextColor(_settings.DefaultButtonTextColor);
        }

        private async Task GenerateWheelEverySecond()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                while (_currentTime > 0)
                {
                    _controller.UpdateCooldownCounter(_currentTime);

                    if (_currentTime > 0)
                    {
                        _wheelGameService.GenerateNewWheel();
                    }

                    _controller.DisplayCurrentWheel();

                    await Task.Delay(1000, _cancellationTokenSource.Token);

                    _currentTime -= 1f;
                }

                _stateMachine.Enter<ActiveState>();
            }
            catch (Exception e)
            {
                Debug.LogError($"[ERROR] GenerateWheelEverySecond: {e.Message} | {e.StackTrace}");
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
            }
        }
    }
}
