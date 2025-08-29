using System.Threading;
using Game.Client.Scripts.Core.AssetSystem;
using Game.Client.Scripts.Core.StateMachine;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Reward;
using Game.Client.Scripts.Features.WheelOfFortune.States;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;

namespace Game.Client.Scripts.Features.WheelOfFortune
{
	public class WheelOfFortuneInitializationService
	{
		private readonly WheelOfFortuneSettings _settings;
		private readonly IWheelView _view;
		private readonly IAssetsProvider _assetsProvider;

		private IWheelGenerator _wheelGenerator;
		private IStateMachine _stateMachine;
		private IWheelController _wheelController;
		private IWheelAnimationController _animationController;
		private IRewardSystem _rewardSystem;
		private IWheelModel _wheelModel;
		private IWheelSpinService _spinService;

		private CancellationToken _viewDestroyCancellationToken;

		public WheelOfFortuneInitializationService(IAssetsProvider assetsProvider, WheelView view, WheelOfFortuneSettings settings)
		{
			_assetsProvider = assetsProvider;
			_view = view;
			_settings = settings;
			_viewDestroyCancellationToken = view.destroyCancellationToken;

			_wheelModel = new WheelModel();
			
			CreateWheelAnimationController();
			CreateRewardGenerator();
			CreateWheelGameService();
			CreateWheelController();
			CreateRewardSystem();
			CreateStateMachine();
			InitializeUI();
			
			_stateMachine.Enter<CooldownState>();
		}

		private void CreateWheelGameService()
		{
			_spinService = new WheelSpinService(_wheelGenerator, _wheelModel);
		}

		private void CreateRewardSystem()
		{
			_rewardSystem = new RewardSystem(_assetsProvider, _wheelController, _wheelModel, _view, _settings);
		}

		private void CreateWheelAnimationController()
		{
			_animationController = new WheelAnimationController(_view, _settings);
		}

		private void CreateRewardGenerator()
		{
			_wheelGenerator = new WheelGenerator(_settings);
		}

		private void CreateWheelController()
		{
			_wheelController = new WheelController(_animationController, _wheelModel, _view, _settings);
			_wheelController.Initialize();
		}

		private void InitializeUI()
		{
			_view.Initialize();
		}

		private void CreateStateMachine()
		{
			_stateMachine = new WheelStateMachine();
			
			_stateMachine.RegisterState<CooldownState>(new CooldownState(_stateMachine, _wheelController, _wheelGenerator, _wheelModel, _settings, _viewDestroyCancellationToken));
			_stateMachine.RegisterState<ActiveState>(new ActiveState(_stateMachine, _wheelController, _spinService, _settings));
			_stateMachine.RegisterState<SpinningState>(new SpinningState(_stateMachine, _wheelController, _settings, _viewDestroyCancellationToken));
			_stateMachine.RegisterState<RewardState>(new RewardState(_stateMachine, _wheelController, _rewardSystem, _settings, _viewDestroyCancellationToken));
		}
	}
}