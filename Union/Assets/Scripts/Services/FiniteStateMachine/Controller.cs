using System;

namespace Union.Services.FiniteStateMachine
{
    public class EventExecutor
    {
		public event Action OnRan;
		public event Action OnEntered;
		public event Action OnExited;

		public void Run()
        {
			OnRan?.Invoke();
        }

		public void Enter()
		{
			OnEntered?.Invoke();
		}

		public void Exit()
		{
			OnExited?.Invoke();
		}
	}
}