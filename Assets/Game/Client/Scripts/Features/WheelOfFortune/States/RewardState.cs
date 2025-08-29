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
	public class RewardState : IState
	{
		private readonly IStateMachine _stateMachine;
		private readonly IWheelController _controller;
		private readonly IRewardSystem _rewardSystem;
		private readonly WheelOfFortuneSettings _settings;
		private readonly CancellationToken _cancellationToken;
		private readonly int _afterRewardDelay;

		public RewardState(IStateMachine stateMachine,
			IWheelController controller,
			IRewardSystem rewardSystem,
			WheelOfFortuneSettings settings, 
			CancellationToken cancellationToken)
		{
			_settings = settings;
			_rewardSystem = rewardSystem;
			_stateMachine = stateMachine;
			_controller = controller;
			_cancellationToken = cancellationToken;
			
			_afterRewardDelay = (int)(_settings.RewardAnimationPause * 1000);
		}

		public void Enter()
		{
			UpdateViewState();

			_rewardSystem.StartRewardAnimationAsync();
			_rewardSystem.OnRewardAnimationCompleted += OnRewardAnimationCompleted;
		}

		private void UpdateViewState()
		{
			_controller.ShowRewardText();
			_controller.HideRewardIcon();
			_controller.SetButtonInteractable(false);
		}

		public void Exit()
		{
			_rewardSystem.OnRewardAnimationCompleted -= OnRewardAnimationCompleted;
		}

		private async void OnRewardAnimationCompleted()
		{
			try
			{
				_rewardSystem.OnRewardAnimationCompleted -= OnRewardAnimationCompleted;

				await Task.Delay(_afterRewardDelay, _cancellationToken);
			
				_stateMachine.Enter<CooldownState>();
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception e)
			{
				Debug.LogError($"[ERROR] {typeof(RewardState)}: {e.Message} | {e.StackTrace}");
			}
		}
	}
}