using System.Collections.Generic;

using Union.Services.FiniteStateMachine;

namespace Union.Services.Charcater.Player
{
    public class FiniteStateMachine
    {
        public static class Constatns
        {
            public const string DieCommand = "die";
        }

        public StateNumber CurrentState
        {
            get
            {
                return this._machine.CurrentState;
            }
        }

        private Player _player;

        private List<KeyValuePair<StateNumber, State>> _states;
        private FiniteStateMachine<StateNumber> _machine;

        public FiniteStateMachine(Player player)
        {
            this._player = player;
        }

        public void Initialize()
        {
            CreateMachine();
            CreateStates();

            SetOnEvent();

            this._machine.SetState(StateNumber.Alive);
        }

        private void CreateMachine()
        {
            this._machine = FiniteStateMachine<StateNumber>.FromEnum()
                .AddTransition(StateNumber.Alive, StateNumber.Dead, Constatns.DieCommand)
                ;
        }

        private void CreateStates()
        {
            this._states = new List<KeyValuePair<StateNumber, State>>();

            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.Alive, new Alive(this._player, this)));
            this._states.Add(new KeyValuePair<StateNumber, State>(StateNumber.Dead, new Dead(this._player, this)));
        }

        private void SetOnEvent()
        {
            foreach (KeyValuePair<StateNumber, State> kvp in this._states)
            {
                this._machine.OnEnter(kvp.Key, kvp.Value.Enter);
                this._machine.OnRun(kvp.Key, kvp.Value.Run);
                this._machine.OnExit(kvp.Key, kvp.Value.Exit);
            }
        }

        public void Run()
        {
            this._machine.Run();
        }

        public void IssueCommand(string command)
        {
            if (this._machine == null)
                return;

            this._machine.IssueCommand(command);
        }
    }
}
