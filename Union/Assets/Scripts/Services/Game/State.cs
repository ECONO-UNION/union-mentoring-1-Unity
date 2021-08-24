using System;
using UnityEngine;

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
        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class Ready : State
    {
        public Ready()
        {

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
        public Playing()
        {

        }

        public override void Enter()
        {
            Debug.Log("OnEnter : Playing");
        }

        public override void Run()
        {
            Logic.Instance.UpdatePlayTime(UnityEngine.Time.deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("OnExit : Playing");
        }
    }

    public class Pause : State
    {
        public Pause()
        {

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
        public End()
        {

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