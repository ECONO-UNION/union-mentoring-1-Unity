using System;

namespace Union.Services.FiniteStateMachine
{
    public abstract class Transition<TState> where TState : IComparable
    {
        public TState FromState { get; private set; }
        public TState ToState { get; private set; }

        private readonly Func<bool> conditionFunction;

        public event Action OnComplete;

        protected Transition(TState fromState, TState toState, Func<bool> conditionFunction = null)
        {
            this.FromState = fromState;
            this.ToState = toState;
            this.conditionFunction = conditionFunction;
        }

        public abstract void Begin();

        protected void Complete()
        {
            if (this.OnComplete != null)
            {
                this.OnComplete();
            }
        }

        public bool IsConditionFunctionExist()
        {
            return this.conditionFunction == null || this.conditionFunction();
        }
    }

    public class DefaultStateTransition<TState> : Transition<TState> where TState : IComparable
    {
        public DefaultStateTransition(TState fromState, TState toState, Func<bool> conditionFunction = null) : base(fromState, toState, conditionFunction)
        {
        }

        public override void Begin()
        {
            Complete();
        }
    }
}