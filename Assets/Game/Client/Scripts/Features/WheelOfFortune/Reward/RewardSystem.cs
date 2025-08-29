using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.AssetSystem;
using Game.Client.Scripts.Core.Pool;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.Factory;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using Game.Client.Scripts.Features.WheelOfFortune.Wheel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
	public class RewardSystem : IRewardSystem
	{
		private readonly IAssetsProvider _assetsProvider;
		private readonly IWheelController _controller;
		private readonly WheelOfFortuneSettings _settings;
		private readonly IWheelView _view;
		private readonly IWheelModel _wheelModel;

		private IObjectPool<RewardObjectView> _pool;
		private RewardCalculator _rewardCalculator;
		private CancellationTokenSource _cancellationTokenSource;
		private List<RewardObjectView> _spawnedRewards = new ();
		private Sprite _currentRewardIcon;
		private int _rewardObjectCount;
		private int _rewardAmount;

		public event Action OnRewardAnimationCompleted;
		
		public RewardSystem(
			IAssetsProvider assetsProvider,
			IWheelController controller,
			IWheelModel model,
			IWheelView view,
			WheelOfFortuneSettings settings)
		{
			_assetsProvider = assetsProvider;
			_wheelModel = model;
			_view = view;
			_controller = controller;
			_settings = settings;

			CreateRewardObjectPool();
			CreateRewardCalculator();
		}

		public void Dispose()
		{
			OnRewardAnimationCompleted = null;
			_pool?.Clear();
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource?.Dispose();
		}
		
		public async Task StartRewardAnimationAsync()
		{
			_cancellationTokenSource = new CancellationTokenSource();

			try
			{
				_rewardAmount = 0;
				_rewardObjectCount = 0;

				GetRewardIcon();
				await SpawnRewardObjectsAsync();
			}
			catch (Exception e)
			{
				Debug.LogError($"[ERROR] StartRewardAnimationAsync: {e.Message} | {e.StackTrace}");
			}
			finally
			{
				_cancellationTokenSource.Dispose();
			}
		}

		private void CreateRewardObjectPool()
		{
			if (string.IsNullOrEmpty(_settings.RewardObjectViewAddressableKey))
			{
				Debug.LogError($"[ERROR] {typeof(RewardObjectView)} Addressable Key is null");
				throw new ArgumentException();
			}
			
			var factory = new WheelRewardViewFactory(_assetsProvider, _settings.RewardObjectViewAddressableKey);
			_pool = new ObjectPool<RewardObjectView>(factory, _settings.MaxRewardObjectsAmount, _settings.MaxRewardObjectsAmount);
		}

		private void CreateRewardCalculator()
		{
			_rewardCalculator = new RewardCalculator(_settings);
		}

		private void GetRewardIcon()
		{
			var rewardType = _wheelModel.CurrentWheelData.RewardType;
			var visualData = _settings.RewardVisuals.FirstOrDefault(x => x.RewardType == rewardType);
            
			if (visualData == null)
			{
				Debug.LogError($"[ERROR] Visual data for reward {rewardType} not found");
				return;
			}

			_currentRewardIcon = visualData.RewardSprite;
		}

		private async Task SpawnRewardObjectsAsync()
		{
			var value = _wheelModel.CurrentSpinResult.WinningValue;
			var distribution = _rewardCalculator.CalculateDistribution(value);
			var rewardValues = _rewardCalculator.FlattenDistributionData(distribution);
			_rewardObjectCount = rewardValues.Count;
            
			foreach (var rewardValue in rewardValues)
			{
				try
				{
					var rewardObj = await SpawnRewardObjectAsync(rewardValue);
					
					if (rewardObj != null)
					{
						_spawnedRewards.Add(rewardObj);
					}
				}
				catch (Exception e)
				{
					Debug.LogError($"[ERROR] Failed to spawn reward object: {e.Message}");
					_rewardObjectCount--;
				}
			}
		}

		private async Task<RewardObjectView> SpawnRewardObjectAsync(int rewardValue)
		{
			var spinResult = _wheelModel.CurrentSpinResult;

			if (spinResult == null)
			{
				Debug.LogError($"[ERROR] Spin result is null");
				return null;
			}
		   
			var randomDirection = Random.insideUnitCircle.normalized;
			var randomRadius = Random.Range(_settings.SpawnRadiusMin, _settings.SpawnRadiusMax);
			var startPosition = spinResult.SpawnPoint.position;
			var targetPosition = spinResult.SpawnPoint.position + (Vector3)(randomDirection * randomRadius);
			var centerPosition = _view.WheelContainer.position;
			var lifeTime = Random.Range(_settings.RewardAfterSpawnPauseTimeMin, _settings.RewardAfterSpawnPauseTimeMax);

			var rewardView = await _pool.GetAsync();

			if (rewardView == null) 
			{
				Debug.LogError("[ERROR] Failed to create reward object from factory");
				return null;
			}
		   
			rewardView.transform.SetParent(_view.Transform);

			var model = new RewardObjectModel(rewardView, new RewardObjectData(_currentRewardIcon, rewardValue));
			model.Spawn(startPosition, targetPosition, centerPosition, lifeTime);
			model.OnAnimationCompleted += OnRewardObjectAnimationCompleted;

			return rewardView;
		}

		private void OnRewardObjectAnimationCompleted(RewardObjectModel model)
		{
			model.OnAnimationCompleted -= OnRewardObjectAnimationCompleted;

			UpdateRewardCounter(model);

			_pool.Return(model.View);
			_rewardObjectCount--;
			
			if (_rewardObjectCount > 0) return;
			
			CompleteRewardAnimation();
		}

		private void UpdateRewardCounter(RewardObjectModel model)
		{
			_rewardAmount += model.RewardData.RewardValue;
			_controller.SetRewardText($"{_rewardAmount}");
		}

		private void CompleteRewardAnimation()
		{
			OnRewardAnimationCompleted?.Invoke();
		}
	}
}