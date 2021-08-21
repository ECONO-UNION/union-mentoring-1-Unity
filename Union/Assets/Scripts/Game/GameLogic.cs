using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        public GameTime GameTime { get; private set; }

        private GameFiniteStateMachine _gameFiniteStateMachine;

        private void Awake()
        {
            this.GameTime = new GameTime();
            this._gameFiniteStateMachine = new GameFiniteStateMachine();
        }

        private void Start()
        {
            this._gameFiniteStateMachine.Initialize();
        }

        private void Update()
        {
            if (IsPlaying() == true)
            {
                this.GameTime.UpdatePlayTime(Time.deltaTime);
            }

            this._gameFiniteStateMachine.Run();
        }

        public bool IsPlaying()
        {
            if (this._gameFiniteStateMachine.CurrentState != States.Playing)
                return false;            

            return true;
        }
    }
}