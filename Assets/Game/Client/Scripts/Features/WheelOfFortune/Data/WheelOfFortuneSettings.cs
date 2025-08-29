using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Data
{
	[CreateAssetMenu(fileName = "new Wheel of fortune settings", menuName = "Game/Wheel of fortune/Settings")]
	public class WheelOfFortuneSettings : ScriptableObject
	{
		[Header("Reward settings")]
		[Min(5)]
		[SerializeField] private int _minRewardAmount = 5;
		[Min(10)]
		[SerializeField] private int _maxRewardAmount = 100;
		[Min(1)]
		[SerializeField] private int _rewardValueStep = 5;
		[Range(1, 20)]
		[SerializeField] private int _maxRewardObjectsAmount = 20;
		
		[Header("Spin settings")]
		[Min(1)]
		[SerializeField] private int _spinCooldownDuration = 10;
		[Min(1)]
		[SerializeField] private float _spinDuration = 5f;
		[Min(0)]
		[SerializeField] private float _rewardAnimationPause = 2f;
		
		[Space]
		[SerializeField] private RewardDataVisual[] _rewardVisuals;
		[SerializeField] private AnimationCurve _spinAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		
		[Space]
		[Header("Reward spawn settings")]
		[SerializeField] private string _rewardObjectAddressableKey;
		
		[SerializeField] private float _spawnRadiusMin = 50;
		[SerializeField] private float _spawnRadiusMax = 250;
		[Min(0)]
		[SerializeField] private float _rewardAfterSpawnPauseTimeMin = 1f;
		[Min(0)]
		[SerializeField] private float _rewardAfterSpawnPauseTimeMax = 2.5f;

		[field: Header("Visual settings")]
		[field: SerializeField]
		public Color DefaultButtonTextColor { get; private set; }  = new Color(0.05f, 0.05f, 0.05f);
		[field: SerializeField]
		public Color CooldownButtonTextColor { get; private set; } = new Color(1f, 0.62f, 0f);

		[field: Header("Text settings")]
		[field: SerializeField]
		public string WheelTitle { get; private set; } = "Рулетка";
		[field: SerializeField]
		public string SpinButtonTitle { get; private set; } = "Испытать удачу";
		
		public string RewardObjectViewAddressableKey => _rewardObjectAddressableKey;

		public int MinRewardAmount => _minRewardAmount;

		public int MaxRewardAmount => _maxRewardAmount;

		public int RewardValueStep => _rewardValueStep;

		public int SpinCooldownDuration => _spinCooldownDuration;

		public float SpinDuration => _spinDuration;

		public float RewardAnimationPause => _rewardAnimationPause;

		public RewardDataVisual[] RewardVisuals => _rewardVisuals;

		public AnimationCurve SpinAnimationCurve => _spinAnimationCurve;

		public int MaxRewardObjectsAmount => _maxRewardObjectsAmount;

		public float SpawnRadiusMin => _spawnRadiusMin;
		public float SpawnRadiusMax => _spawnRadiusMax;

		public float RewardAfterSpawnPauseTimeMin => _rewardAfterSpawnPauseTimeMin;

		public float RewardAfterSpawnPauseTimeMax => _rewardAfterSpawnPauseTimeMax;
	}
}