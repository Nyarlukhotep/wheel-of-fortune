using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Client.Scripts.Core.AssetSystem
{
	public interface IAssetsProvider : IDisposable
	{
		Task<GameObject> LoadAsset(string address);
		void ReleaseAsset(GameObject asset);
		void Cleanup();
	}
}