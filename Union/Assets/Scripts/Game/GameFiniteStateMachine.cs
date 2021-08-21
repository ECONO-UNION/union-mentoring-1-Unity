using System.Collections.Generic;
using UnityEngine;

using Union.Services.FiniteStateMachine;

namespace Union.Services.Game
{
    public class GameFiniteStateMachine
    {
        public static class Constants
        {
            public const string StartCommand = "start";
            public const string PauseCommand = "pause";
            public const string ResumeCommand = "resume";
            public const string EndCommand = "end";
        }

        public States CurrentState
        {
            get
            {
                return this._machine.CurrentState;
            }
        }

        private List<KeyValuePair<States, State>> _states;
        private Machine<States> _machine;

        public void Run()
        {
            this._machine.Run();
        }

        public void Initialize()
        {
            CreateMachine();
            CreateStates();

            SetOnEvent();
            
            this._machine.SetState(States.Ready);
            this._machine.IssueCommand(Constants.StartCommand);
        }

        private void CreateMachine()
        {
            this._machine = Machine<States>.FromEnum()
                .AddTransition(States.Ready, States.Playing, Constants.StartCommand)
                .AddTransition(States.Playing, States.Pause, Constants.PauseCommand)
                .AddTransition(States.Pause, States.Playing, Constants.ResumeCommand)
                .AddTransition(States.Playing, States.End, Constants.EndCommand)
                ;
        }

        private void CreateStates()
        {
            this._states = new List<KeyValuePair<States, State>>();

            this._states.Add(new KeyValuePair<States, State>(States.Ready, new Ready(this)));
            this._states.Add(new KeyValuePair<States, State>(States.Playing, new Playing(this)));
            this._states.Add(new KeyValuePair<States, State>(States.Pause, new Pause(this)));
            this._states.Add(new KeyValuePair<States, State>(States.End, new End(this)));
        }

        private void SetOnEvent()
        {
            foreach (KeyValuePair<States, State> kvp in this._states)
            {
                this._machine.OnEnter(kvp.Key, kvp.Value.Enter);
                this._machine.OnRun(kvp.Key, kvp.Value.Run);
                this._machine.OnExit(kvp.Key, kvp.Value.Exit);
            }
        }

        public void IssueCommand(string command)
        {
            if (this._machine == null)
                return;

            this._machine.IssueCommand(command);
        }

        public States GetCurrentState()
        {
            return this._machine.CurrentState;
        }
    }
}