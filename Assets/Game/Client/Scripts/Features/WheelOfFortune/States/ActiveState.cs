using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
    public class ActiveState : IState
    {
        private readonly WheelController _controller;
        private readonly IStateMachine _stateMachine;
        private readonly WheelGameService _wheelGameService;
        private readonly WheelOfFortuneSettings _settings;

        public ActiveState(
            IStateMachine stateMachine,
            WheelController controller,
            WheelGameService wheelGameService,
            WheelOfFortuneSettings settings)
        {
            _settings = settings;
            _wheelGameService = wheelGameService;
            _stateMachine = stateMachine;
            _controller = controller;
        }

        public void Enter()
        {
            _controller.SetButtonInteractable(true);
            _controller.SetButtonText(_settings.SpinButtonTitle);
            _controller.DisplayCurrentWheel();

            _controller.OnSpinButtonPressed += StartSpin;
        }

        public void Exit()
        {
            _controller.OnSpinButtonPressed -= StartSpin;
        }

        private void StartSpin()
        {
            _wheelGameService.DetermineSpinResult(_controller);
            
            _stateMachine.Enter<SpinningState>();
        }
    }
}
