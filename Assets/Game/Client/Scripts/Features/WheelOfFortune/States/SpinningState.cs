using System;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
    public class SpinningState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IWheelController _controller;
        private readonly WheelOfFortuneSettings _settings;

        public SpinningState(IStateMachine stateMachine, IWheelController controller, WheelOfFortuneSettings settings)
        {
            _stateMachine = stateMachine;
            _controller = controller;
            _settings = settings;
        }

        public async void Enter()
        {
            try
            {
                _controller.SetButtonInteractable(false);

                var delay = (int)(_settings.SpinDuration * 1000 + 1000); //TODO:
                await Task.Delay(delay);
            
                _stateMachine.Enter<RewardState>();
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
