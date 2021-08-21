using UnityEngine;
using UnityEngine.Assertions;

namespace Union.Services.Charcater.Player
{
    public enum States
    {
        Alive,
        Dead,
    }

    public abstract class State
    {
        protected Player _player { get; set; }
        protected FiniteStateMachine _finiteStateMachine { get; set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Alive : State
    {
        public Alive(Player player, FiniteStateMachine finiteStateMachine)
        {
            this._player = player;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Player Alive");
        }

        public override void Run()
        {
            if (this._player.BaseStat.HealthPoint.Get() <= 0)
            {
                this._finiteStateMachine.IssueCommand(FiniteStateMachine.Constatns.DieCommand);
                Assert.AreEqual(this._finiteStateMachine.CurrentState, States.Dead);
            }
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Player Alive");
        }
    }

    public class Dead : State
    {
        public Dead(Player player, FiniteStateMachine finiteStateMachine)
        {
            this._player = player;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Player Dead");

            this._player.enabled = false;
            this._player.gameObject.SetActive(false);
        }

        public override void Run()
        {

        }

        public override void Exit()
        {
            Debug.Log("OnExit : Player Dead");
        }
    }
}
