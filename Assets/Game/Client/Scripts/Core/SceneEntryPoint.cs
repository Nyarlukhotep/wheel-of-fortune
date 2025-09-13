using Game.Client.Scripts.Core.AssetSystem;
using Game.Client.Scripts.Features.WheelOfFortune;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;

namespace Game.Client.Scripts.Core
{
	public class SceneEntryPoint : MonoBehaviour
	{
		[SerializeField] private WheelOfFortuneSettings _wheelOfFortuneSettings;
		[SerializeField] private WheelView _wheelView;
		
		private IAssetsProvider _assetsProvider;

		private void Start()
		{
			PreInit();
			Init();
		}

		private void PreInit()
		{
			CreateAssetsProvider();
		}

		private void Init()
		{
			InitWheelOfFortune();
		}

		private void CreateAssetsProvider()
		{
			_assetsProvider = new AddressableAssetsProvider();
		}

		private void InitWheelOfFortune()
		{
			new WheelOfFortuneInitializationService(_assetsProvider, _wheelView, _wheelOfFortuneSettings);
		}
	}
}