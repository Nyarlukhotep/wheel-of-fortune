using System;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.AssetSystem;
using Game.Client.Scripts.Core.Factory;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Client.Scripts.Features.WheelOfFortune.Factory
{
	public class WheelRewardViewFactory : IObjectFactory<RewardObjectView>
	{
		private readonly IAssetsProvider _assetProvider;
		private readonly string _assetAddress;
		private GameObject _prefab;

		public WheelRewardViewFactory(IAssetsProvider assetProvider, string assetAddress)
		{
			_assetProvider = assetProvider;
			_assetAddress = assetAddress;
		}

		public async Task PreloadPrefab()
		{
			_prefab = await _assetProvider.LoadAsset(_assetAddress);
		}

		public async Task<RewardObjectView> Create()
		{
			if (_prefab == null)
			{
				await PreloadPrefab();
			}

			var operation = Object.InstantiateAsync(_prefab);
			
			while (!operation.isDone)
			{
				await Task.Yield();
			}

			var instance = operation.Result[0];
			var rewardView = instance.GetComponent<RewardObjectView>();

			if (rewardView != null)
			{
				return rewardView;
			}
			
			Object.Destroy(instance);
			throw new Exception($"Prefab at address {_assetAddress} doesn't have {typeof(RewardObjectView)} component");
		}

		public void Release(RewardObjectView obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj.gameObject);
			}
		}

		public void Dispose()
		{
			_prefab = null;
			_assetProvider?.Dispose();
		}
	}
}