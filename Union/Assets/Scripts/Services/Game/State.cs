using UnityEngine;
using UnityEngine.Assertions;

namespace Union.Services.Game
{
    public enum States
    {
        Ready,
        Playing,
        Pause,
        End,
    }

    public abstract class State
    {
        protected Logic _logic { get; set; }
        protected FiniteStateMachine _finiteStateMachine { get; set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Ready : State
    {
        public Ready(Logic logic, FiniteStateMachine finiteStateMachine)
        {
            this._logic = logic;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Ready");
        }

        public override void Run()
        {

        }

        public override void Exit()
        {
            Debug.Log("OnExit : Ready");
        }
    }

    public class Playing : State
    {
        public Playing(Logic logic, FiniteStateMachine finiteStateMachine)
        {
            this._logic = logic;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Playing");
        }

        public override void Run()
        {
            this._logic.UpdatePlayTime(UnityEngine.Time.deltaTime);

            if (BattleField.Instance.currentEnemyCount <= 0)
            {
                this._finiteStateMachine.IssueCommand(FiniteStateMachine.Constants.EndCommand);
                Assert.AreEqual(this._finiteStateMachine.CurrentStates, States.End);
            }
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Playing");
        }
    }

    public class Pause : State
    {
        public Pause(Logic logic, FiniteStateMachine finiteStateMachine)
        {
            this._logic = logic;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Pause");
        }

        public override void Run()
        {

        }

        public override void Exit()
        {
            Debug.Log("OnExit : Pause");
        }
    }

    public class End : State
    {
        public End(Logic logic, FiniteStateMachine finiteStateMachine)
        {
            this._logic = logic;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : End");
        }

        public override void Run()
        {

        }

        public override void Exit()
        {
            Debug.Log("OnExit : End");
        }
    }

}