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
            this._gameFiniteStateMachine = new GameFiniteStateMachine(this);
        }

        private void Start()
        {
            this._gameFiniteStateMachine.Initialize();
        }

        private void Update()
        {
            this._gameFiniteStateMachine.Run();
        }

        public void UpdatePlayTime(float addTime)
        {
            this.GameTime.UpdatePlayTime(addTime);
        }

        public bool IsPlaying()
        {
            if (this._gameFiniteStateMachine.CurrentStates != States.Playing)
                return false;            

            return true;
        }
    }
}