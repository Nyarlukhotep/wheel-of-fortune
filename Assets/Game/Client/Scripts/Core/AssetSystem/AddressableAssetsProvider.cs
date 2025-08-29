using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Client.Scripts.Core.AssetSystem
{
	public class AddressableAssetsProvider : IAssetsProvider
	{
		private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _handles = new ();
		private readonly Dictionary<string, GameObject> _preloadedAssets = new ();

		public async Task<GameObject> LoadAsset(string address)
		{
			if (_preloadedAssets.TryGetValue(address, out var preloadedAsset))
			{
				return preloadedAsset;
			}

			if (_handles.ContainsKey(address) && _handles[address].IsValid())
			{
				await _handles[address].Task;
				return _handles[address].Result;
			}

			var handle = Addressables.LoadAssetAsync<GameObject>(address);
			_handles[address] = handle;
	        
			await handle.Task;

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				throw new Exception($"Failed to load asset: {address}");
			}
	        
			return handle.Result;
		}

		public async Task PreloadAsset(string address)
		{
			if (!_preloadedAssets.ContainsKey(address))
			{
				var asset = await LoadAsset(address);
				_preloadedAssets[address] = asset;
			}
		}

		public void ReleasePreloadedAsset(string address)
		{
			if (!_preloadedAssets.TryGetValue(address, out var asset)) return;
		    
			if (_handles.TryGetValue(address, out var handle))
			{
				Addressables.Release(handle);
				_handles.Remove(address);
			}
		    
			_preloadedAssets.Remove(address);
		}

		public void ReleaseAsset(GameObject asset)
		{
		}
	    
		public void Cleanup()
		{
			foreach (var handle in _handles.Values)
			{
				Addressables.Release(handle);
			}
        
			_handles.Clear();
			_preloadedAssets.Clear();
		}

		public void Dispose()
		{
			Cleanup();
		}
	}
}