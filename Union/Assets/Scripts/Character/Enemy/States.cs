using UnityEngine;
using UnityEngine.Assertions;

namespace Union.Services.Charcater.Enemy
{
    public enum States
    {
        Alive,
        Dead,
    }

    public abstract class State
    {
        protected Enemy _enemy { get; set; }
        protected FiniteStateMachine _finiteStateMachine { get; set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Alive : State
    {
        public Alive(Enemy enemy, FiniteStateMachine finiteStateMachine)
        {
            this._enemy = enemy;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Enemy Alive");
        }

        public override void Run()
        {
            if (this._enemy.BaseStat.HealthPoint.Get() <= 0)
            {
                this._finiteStateMachine.IssueCommand(FiniteStateMachine.Constatns.DieCommand);
                Assert.AreEqual(this._finiteStateMachine.CurrentState, States.Dead);
            }
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Enemy Alive");
        }
    }

    public class Dead : State
    {
        public Dead(Enemy enemy, FiniteStateMachine finiteStateMachine)
        {
            this._enemy = enemy;
            this._finiteStateMachine = finiteStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Enemy Dead");

            Union.Services.Game.BattleField.Instance.DecreaseEnemyCount(1);
            this._enemy.enabled = false;
            this._enemy.gameObject.SetActive(false);
        }

        public override void Run()
        {

        }

        public override void Exit()
        {
            Debug.Log("OnExit : Enemy Dead");
        }
    }
}
