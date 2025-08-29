using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Client.Scripts.Features.WheelOfFortune.UI
{
	[RequireComponent(typeof(Button))]
	public class SpinButton : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _buttonText;
		
		private Action _callback;

		public void SetText(string text)
		{
			_buttonText?.SetText(text);
		}

		public void SetButtonTextColor(Color color)
		{
			_buttonText.color = color;
		}

		public void SetCallback(Action callback)
		{
			_callback = callback;
		}

		public void SetInteractable(bool interactable)
		{
			_button.interactable = interactable;
		}

		private void Awake()
		{
			if (_button == null)
			{
				Debug.LogError($"[ERROR SpinButton: assign Button component]");
				return;
			}
			
			_button.onClick.AddListener(OnButtonPressed);
		}

		private void OnDestroy()
		{
			if (_button != null)
			{
				_button.onClick.RemoveAllListeners();
			}
		}

		private void OnButtonPressed()
		{
			_callback?.Invoke();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_button = GetComponent<Button>();
		}
#endif
	}
}