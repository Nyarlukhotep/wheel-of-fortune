using System;
using System.Collections.Generic;
using Game.Client.Scripts.Core.StateMachine;
using UnityEngine;

namespace Game.Client.Scripts.Features.WheelOfFortune.States
{
	public class WheelStateMachine : IStateMachine
	{
		private readonly Dictionary<Type, IState> _states = new ();
		private IState _activeState;

		public WheelStateMachine()
		{
			_states = new Dictionary<Type, IState>();
		}
		
		public void RegisterState<T>(IState state)
		{
			var type = typeof(T);
			
			_states.TryAdd(type, state);
		}
		
		public void Enter<TState>() where TState : class, IState
		{
			var state = ChangeState<TState>();
			state?.Enter();
		}

		private TState ChangeState<TState>() where TState : class, IState
		{
			Debug.Log($"<color=#FF891C>Game state exit</color> => {_activeState}");
			_activeState?.Exit();
      
			var state = GetState<TState>();
			_activeState = state;

			Debug.Log($"<color=#6DFF23>Game state enter</color> => {_activeState}");
      
			return state;
		}
		
		private TState GetState<TState>() where TState : class, IState
		{
			if (!_states.TryGetValue(typeof(TState), out var state))
			{
				throw new NullReferenceException($"[ERROR] State of type {typeof(TState)} not exist");
			}
			
			return state as TState;
		}
	}
}