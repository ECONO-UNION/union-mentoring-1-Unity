using System.Collections.Generic;

using Union.Services.FiniteStateMachine;

namespace Union.Services.Game
{
    public class FiniteStateMachineController
    {
        public static class Constants
        {
            public const string StartCommand = "start";
            public const string PauseCommand = "pause";
            public const string ResumeCommand = "resume";
            public const string EndCommand = "end";
        }

        public States CurrentStates
        {
            get
            {
                return this._finiteStateMachine.CurrentState;
            }
        }

        private List<KeyValuePair<States, State>> _states;
        private FiniteStateMachine<States> _finiteStateMachine;

        public void Initialize()
        {
            CreateMachine();
            CreateStates();

            SetOnEvent();
            
            this._finiteStateMachine.SetState(States.Ready);
            this._finiteStateMachine.IssueCommand(Constants.StartCommand);
        }

        private void CreateMachine()
        {
            this._finiteStateMachine = FiniteStateMachine<States>.FromEnum()
                .AddTransition(States.Ready, States.Playing, Constants.StartCommand)
                .AddTransition(States.Playing, States.Pause, Constants.PauseCommand)
                .AddTransition(States.Pause, States.Playing, Constants.ResumeCommand)
                .AddTransition(States.Playing, States.End, Constants.EndCommand, EndCommandCondition)
                ;
        }

        private bool EndCommandCondition()
        {
            if (BattleField.Instance.currentEnemyCount <= 0)
            {
                return true;
            }

            return false;
        }

        private void CreateStates()
        {
            this._states = new List<KeyValuePair<States, State>>();

            this._states.Add(new KeyValuePair<States, State>(States.Ready, new Ready()));
            this._states.Add(new KeyValuePair<States, State>(States.Playing, new Playing()));
            this._states.Add(new KeyValuePair<States, State>(States.Pause, new Pause()));
            this._states.Add(new KeyValuePair<States, State>(States.End, new End()));
        }

        private void SetOnEvent()
        {
            foreach (KeyValuePair<States, State> kvp in this._states)
            {
                this._finiteStateMachine.OnEnter(kvp.Key, kvp.Value.Enter);
                this._finiteStateMachine.OnRun(kvp.Key, kvp.Value.Run);
                this._finiteStateMachine.OnExit(kvp.Key, kvp.Value.Exit);
            }
        }

        public void Run()
        {
            this._finiteStateMachine.Run();
        }
    }
}