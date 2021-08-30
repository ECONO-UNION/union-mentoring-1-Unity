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

        public StateNumber CurrentStateNumber
        {
            get
            {
                return this._finiteStateMachine.CurrentState;
            }
        }

        private List<KeyValuePair<StateNumber, State>> _states;
        private FiniteStateMachine<StateNumber> _finiteStateMachine;

        public FiniteStateMachineController()
        {
            CreateMachine();
            CreateStates();

            SetOnEvent();
        }

        public void Initialize()
        {
            this._finiteStateMachine.SetState(StateNumber.Ready);
            this._finiteStateMachine.IssueCommand(Constants.StartCommand);
        }

        private void CreateMachine()
        {
            this._finiteStateMachine = FiniteStateMachine<StateNumber>.FromEnum()
                .AddTransition(StateNumber.Ready, StateNumber.Playing, Constants.StartCommand)
                .AddTransition(StateNumber.Playing, StateNumber.Pause, Constants.PauseCommand)
                .AddTransition(StateNumber.Pause, StateNumber.Playing, Constants.ResumeCommand)
                .AddTransition(StateNumber.Playing, StateNumber.End, Constants.EndCommand, EndCommandCondition)
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
            this._states = new List<KeyValuePair<StateNumber, State>>();

            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.Ready, new Ready()));
            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.Playing, new Playing()));
            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.Pause, new Pause()));
            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.End, new End()));
        }

        private void SetOnEvent()
        {
            foreach (KeyValuePair<StateNumber, State> kvp in this._states)
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