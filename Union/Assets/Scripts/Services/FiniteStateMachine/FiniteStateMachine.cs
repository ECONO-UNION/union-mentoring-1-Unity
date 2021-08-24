using System;
using System.Collections.Generic;

using UnityEngine;

/*
 * reference git : https://github.com/dubit/unity-fsm
 */
namespace Union.Services.FiniteStateMachine
{ 
    public class FiniteStateMachine<TState> where TState : IComparable
    {
        public TState CurrentState { get; private set; }

        private Transition<TState> _currentTransition;

        private readonly Dictionary<TState, Dictionary<string, Transition<TState>>> _transitions;
        private readonly Dictionary<TState, Controller> _controller;

        private event Action<TState, TState> _onStateChange;

        private bool _isInitialisingState;

        public FiniteStateMachine(params TState[] states)
        {
            if (states.Length < 1)
            {
                throw new ArgumentException("A FiniteStateMachine needs at least 1 state", "states");
            }

            this._transitions = new Dictionary<TState, Dictionary<string, Transition<TState>>>();
            this._controller = new Dictionary<TState, Controller>();

            foreach (var value in states)
            {
                this._transitions.Add(value, new Dictionary<string, Transition<TState>>());
                this._controller.Add(value, new Controller());
            }
        }

        public static FiniteStateMachine<TState> FromEnum()
        {
            if (typeof(Enum).IsAssignableFrom(typeof(TState)) == false)
            {
                throw new Exception("Can't create finite");
            }

            List<TState> states = new List<TState>();
            foreach (TState value in Enum.GetValues(typeof(TState)))
            {
                states.Add(value);
            }

            return new FiniteStateMachine<TState>(states.ToArray());
        }

        public FiniteStateMachine<TState> AddTransition(TState fromState, TState toState, string command, Transition<TState> transition = null)
        {
            if (this._transitions.ContainsKey(fromState) == false)
            {
                throw new ArgumentException("unknown state", "from");
            }

            if (this._transitions.ContainsKey(toState) == false)
            {
                throw new ArgumentException("unknown state", "to");
            }

            this._transitions[fromState][command] = transition ?? new DefaultStateTransition<TState>(fromState, toState);

            return this;
        }

        public FiniteStateMachine<TState> AddTransition(TState fromState, TState toState, string command, Func<bool> condition)
        {
            if (this._transitions.ContainsKey(fromState) == false)
            {
                throw new ArgumentException("unknown state", "from");
            }

            if (this._transitions.ContainsKey(toState) == false)
            {
                throw new ArgumentException("unknown state", "to");
            }

            this._transitions[fromState][command] = new DefaultStateTransition<TState>(fromState, toState, condition);

            return this;
        }

        public FiniteStateMachine<TState> OnRun(TState state, Action handler)
        {
            if (this._transitions.ContainsKey(state) == false)
            {
                throw new ArgumentException("unknown state", "state");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this._controller[state].OnRan += handler;

            return this;
        }

        public FiniteStateMachine<TState> OnEnter(TState state, Action handler)
        {
            if (this._transitions.ContainsKey(state) == false)
            {
                throw new ArgumentException("unknown state", "state");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this._controller[state].OnEntered += handler;

            return this;
        }

        public FiniteStateMachine<TState> OnExit(TState state, Action handler)
        {
            if (this._transitions.ContainsKey(state) == false)
            {
                throw new ArgumentException("unknown state", "state");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this._controller[state].OnExited += handler;

            return this;
        }

        public FiniteStateMachine<TState> OnChange(Action<TState, TState> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this._onStateChange += handler;

            return this;
        }

        public FiniteStateMachine<TState> OnChange(TState fromState, TState toState, Action handler)
        {
            if (this._transitions.ContainsKey(fromState) == false)
            {
                throw new ArgumentException("unknown state", "from");
            }

            if (this._transitions.ContainsKey(toState) == false)
            {
                throw new ArgumentException("unknown state", "to");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this._onStateChange += (fromState, toState) =>
            {
                if (fromState.Equals(fromState) && toState.Equals(toState))
                {
                    handler();
                }
            };

            return this;
        }

        public void SetState(TState state)
        {
            if (this._transitions.ContainsKey(state) == false)
            {
                throw new ArgumentException("unknown state", "state");
            }

            this.CurrentState = state;
        }

        public void Run()
        {
            if (this._transitions.ContainsKey(this.CurrentState) == false)
            {
                throw new ArgumentException("unknown state", "state");
            }

            this._controller[this.CurrentState].Run();

            CheckConditionAndTransition();
        }

        private void CheckConditionAndTransition()
        {
            if (this._currentTransition != null)
                return;

            if (this._isInitialisingState == true)
            {
                Debug.LogWarning("Do not call IssueCommand from OnStateChange and OnStateEnter handlers");
                return;
            }

            var transitionsForCurrentState = this._transitions[this.CurrentState];
            foreach (KeyValuePair<string, Transition<TState>> item in transitionsForCurrentState)
            {
                var transition = item.Value;
                if (transition.IsNoCondition() == true)
                    continue;

                if (transition.IsMeetCondition() == true)
                {
                    transition.OnComplete += HandleTransitionComplete;
                    this._currentTransition = transition;
                    this._controller[this.CurrentState].Exit();
                    transition.Begin();
                    return;
                }                    
            }
        }

        public void IssueCommand(string command)
        {
            if (this._currentTransition != null)
                return;

            if (this._isInitialisingState == true)
            {
                Debug.LogWarning("Do not call IssueCommand from OnStateChange and OnStateEnter handlers");
                return;
            }

            var transitionsForCurrentState = this._transitions[this.CurrentState];
            if (transitionsForCurrentState.ContainsKey(command) == false)
                return;

            var transition = transitionsForCurrentState[command];
            if (transition.IsNoCondition() == true || transition.IsMeetCondition() == true)
            {
                transition.OnComplete += HandleTransitionComplete;
                this._currentTransition = transition;
                this._controller[this.CurrentState].Exit();
                transition.Begin();
            }
        }

        public void HandleTransitionComplete()
        {
            this._currentTransition.OnComplete -= HandleTransitionComplete;

            var previousState = this.CurrentState;
            this.CurrentState = _currentTransition.ToState;

            this._currentTransition = null;
            this._isInitialisingState = true;

            if (this._onStateChange != null)
            {
                this._onStateChange(previousState, this.CurrentState);
            }

            this._controller[this.CurrentState].Enter();
            this._isInitialisingState = false;
        }
    }
}