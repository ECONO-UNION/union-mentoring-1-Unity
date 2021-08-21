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
        protected GameFiniteStateMachine _gameFiniteStateMachine { get; set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Ready : State
    {
        public Ready(GameFiniteStateMachine gameFiniteStateMachine)
        {
            this._gameFiniteStateMachine = gameFiniteStateMachine;
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
        public Playing(GameFiniteStateMachine gameFiniteStateMachine)
        {
            this._gameFiniteStateMachine = gameFiniteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Playing");
        }

        public override void Run()
        {
            if (BattleField.Instance.currentEnemyCount <= 0)
            {
                this._gameFiniteStateMachine.IssueCommand(GameFiniteStateMachine.Constants.EndCommand);
                Assert.AreEqual(this._gameFiniteStateMachine.GetCurrentState(), States.End);
            }
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Playing");
        }
    }

    public class Pause : State
    {
        public Pause(GameFiniteStateMachine gameFiniteStateMachine)
        {
            this._gameFiniteStateMachine = gameFiniteStateMachine;
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
        public End(GameFiniteStateMachine gameFiniteStateMachine)
        {
            this._gameFiniteStateMachine = gameFiniteStateMachine;
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