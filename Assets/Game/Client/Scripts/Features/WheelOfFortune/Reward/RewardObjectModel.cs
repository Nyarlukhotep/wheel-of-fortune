using System;
using System.Threading.Tasks;
using DG.Tweening;
using Game.Client.Scripts.Constants;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
	public class RewardObjectModel : IDisposable
	{
		private Vector3 _targetPosition;
		private Vector3 _centerPosition;
		private bool _isMovingToCenter;
        
		private RewardObjectView _view;
		private RewardObjectData _rewardData;

		public RewardObjectView View => _view;
		public RewardObjectData RewardData => _rewardData;

		public event Action<RewardObjectModel> OnAnimationCompleted;
        
		public RewardObjectModel(RewardObjectView view, RewardObjectData rewardData)
		{
			_view = view;
			_rewardData = rewardData;
			
			UpdateIcon();
		}

		public void Dispose()
		{
			_view = null;
		}

		public async Task Spawn(Vector3 startPosition, Vector3 targetPosition, Vector3 centerPosition, float lifeTime)
		{
			try
			{
				_targetPosition = targetPosition;
				_centerPosition = centerPosition;
            
				_view.transform.position = startPosition;
				_view.transform.localScale = Vector3.zero;
            
				_view.transform.DOScale(1, GameConstants.WheelOfFortune.REWARD_OBJECT_SPAWN_ANIMATION_DURATION);
				_view.transform.DOMove(_targetPosition, GameConstants.WheelOfFortune.REWARD_OBJECT_SPAWN_ANIMATION_DURATION);

				var delay = (int)((lifeTime + GameConstants.WheelOfFortune.REWARD_OBJECT_SPAWN_ANIMATION_DURATION) * 1000);
				
				await Task.Delay(delay, _view.destroyCancellationToken);

				MoveToCenter();
				
				delay = (int)(GameConstants.WheelOfFortune.REWARD_OBJECT_HIDE_ANIMATION_DURATION * 1000);
				
				await Task.Delay(delay, _view.destroyCancellationToken);
				
				OnAnimationCompleted?.Invoke(this);
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception e)
			{
				Debug.LogError($"[ERROR] RewardObjectModel::Spawn: {e.Message}");
			}
		}

		private void MoveToCenter()
		{
			_view.transform.DOScale(0, GameConstants.WheelOfFortune.REWARD_OBJECT_HIDE_ANIMATION_DURATION);
			_view.transform.DOMove(_centerPosition, GameConstants.WheelOfFortune.REWARD_OBJECT_HIDE_ANIMATION_DURATION);
		}

        private void UpdateIcon()
		{
			_view.SetIcon(_rewardData.RewardIcon);
		}
	}
}