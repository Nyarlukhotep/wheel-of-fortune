using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
    public class SpinningState : IState
    {
        private readonly IWheelController _controller;
        private readonly WheelOfFortuneSettings _settings;
        private readonly CancellationToken _cancellationToken;
        private IStateMachine _stateMachine;


        public SpinningState(
            IWheelController controller,
            WheelOfFortuneSettings settings,
            CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _controller = controller;
            _settings = settings;
        }

        public void Register(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            try
            {
                _controller?.SetButtonInteractable(false);

                await Task.Delay(TimeSpan.FromSeconds(_settings.SpinDuration), _cancellationToken);

                if (_controller != null)
                {
                    _stateMachine.Enter<RewardState>();
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError($"[ERROR] {typeof(SpinningState)}: {e.Message} | {e.StackTrace}");
            }
        }

        public void Exit()
        {
        }
    }
}
