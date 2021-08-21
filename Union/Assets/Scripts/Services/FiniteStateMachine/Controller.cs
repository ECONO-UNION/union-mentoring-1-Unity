using System;

namespace Union.Services.FiniteStateMachine
{
    public class Controller
    {
		public event Action OnRan;
		public event Action OnEntered;
		public event Action OnExited;

		public void Run()
        {
			if (OnRan != null) OnRan();
        }

		public void Enter()
		{
			if (OnEntered != null) OnEntered();
		}

		public void Exit()
		{
			if (OnExited != null) OnExited();
		}
	}
}