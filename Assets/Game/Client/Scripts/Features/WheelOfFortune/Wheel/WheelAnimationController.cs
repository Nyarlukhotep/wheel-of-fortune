using System;
using DG.Tweening;
using Game.Client.Scripts.Constants;
using Game.Client.Scripts.Features.WheelOfFortune.Data;
using Game.Client.Scripts.Features.WheelOfFortune.UI;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.Wheel
{
	public class WheelAnimationController
	{
		private readonly Transform _wheelContainer;
		private readonly float _slotAngle;
		private readonly WheelOfFortuneSettings _settings;
		private Action _onCompletedCallback;
		
		public WheelAnimationController(WheelView view, WheelOfFortuneSettings settings)
		{
			_settings = settings;
			_wheelContainer = view.WheelContainer;

			_slotAngle = GameConstants.WheelOfFortune.ONE_TURN_DEGREES / GameConstants.WheelOfFortune.SECTORS_COUNT;
			SetStartRotation();
		}

		public void StartWheelSpinning(int targetSector, Action onCompletedCallback)
		{
			_onCompletedCallback = onCompletedCallback;
            
			var targetAngle = CalculateTargetAngle(targetSector);
			
			_wheelContainer
				.DORotate(new Vector3(0, 0, targetAngle), _settings.SpinDuration, RotateMode.FastBeyond360)
				.SetEase(_settings.SpinAnimationCurve)
				.OnComplete(OnSpinningComplete);
		}

		private void SetStartRotation()
		{
			_wheelContainer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _slotAngle * 0.5f));
		}

		private void OnSpinningComplete()
		{
			_onCompletedCallback?.Invoke();
		}

		private float CalculateTargetAngle(int sectorId)
		{
			var targetAngle = sectorId * _slotAngle + _slotAngle * 0.5f + GameConstants.WheelOfFortune.BASE_WHEEL_ROTATE_ANGLE;
			return targetAngle;
		}
	}
}