namespace Game.Client.Scripts.Core.StateMachine
{
	public interface IStateMachine
	{
		void RegisterState<T>(IState state);
		void Enter<TState>() where TState : class, IState;
	}
}