using UnityEngine;

namespace Union.Services.Charcater.Player
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
            Debug.Log("OnEnter : Player Alive");
        }

        public override void Run()
        {
            
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Player Alive");
        }
    }

    public class Dead : State
    {
        public Dead()
        {

        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Player Dead");
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
