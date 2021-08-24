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

        public States CurrentState
        {
            get
            {
                return this._machine.CurrentState;
            }
        }

        private Player _player;

        private List<KeyValuePair<States, State>> _states;
        private FiniteStateMachine<States> _machine;

        public FiniteStateMachine(Player player)
        {
            this._player = player;
        }

        public void Initialize()
        {
            CreateMachine();
            CreateStates();

            SetOnEvent();

            this._machine.SetState(States.Alive);
        }

        private void CreateMachine()
        {
            this._machine = FiniteStateMachine<States>.FromEnum()
                .AddTransition(States.Alive, States.Dead, Constatns.DieCommand)
                ;
        }

        private void CreateStates()
        {
            this._states = new List<KeyValuePair<States, State>>();

            this._states.Add(new KeyValuePair<States, State>(States.Alive, new Alive(this._player, this)));
            this._states.Add(new KeyValuePair<States, State>(States.Dead, new Dead(this._player, this)));
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
