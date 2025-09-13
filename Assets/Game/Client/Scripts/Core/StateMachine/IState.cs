namespace Game.Client.Scripts.Core.StateMachine
{
	public interface IState
	{
		void Register(IStateMachine stateMachine);
		void Enter();
		void Exit();
	}
}