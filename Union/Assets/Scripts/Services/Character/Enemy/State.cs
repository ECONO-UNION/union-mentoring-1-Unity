using UnityEngine;

namespace Union.Services.Charcater.Enemy
{
    public enum StateNumber
    {
        Alive,
        Dead,
    }

    public abstract class State
    {
        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Alive : State
    {
        public Alive()
        {
            
        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Enemy Alive");
        }

        public override void Run()
        {
            
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Enemy Alive");
        }
    }

    public class Dead : State
    {
        public Dead()
        {

        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Enemy Dead");

            Game.BattleField.Instance.DecreaseEnemyCount(1);
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
