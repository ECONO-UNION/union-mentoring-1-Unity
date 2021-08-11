using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        private static class Constants
        {
            public const int BattleFieldEnemyCount = 1;
        }

        private GameState _GameState;

        public GameTime gameTime;

        private void Awake()
        {
            this.gameTime = new GameTime();
        }

        private void Start()
        {
            SetState(GameStates.Playing);
        }

        private void Update()
        {
            if (IsPlaying() == true)
            {
                this.gameTime.UpdatePlayTime(Time.deltaTime);
            }

            this._GameState.Run();
        }

        public void SetState(GameStates gameStates)
        {
            this._GameState?.Exit();
            CreateIGameState(gameStates);
            this._GameState.Enter();
        }

        private void CreateIGameState(GameStates gameStates)
        {
            switch (gameStates)
            {
                case GameStates.Ready:
                    this._GameState = new ReadyGame(this);
                    break;
                case GameStates.Start:
                    this._GameState = new StartGame(this);
                    break;
                case GameStates.Playing:
                    this._GameState = new PlayingGame(this);
                    break;
                case GameStates.Pause:
                    this._GameState = new PauseGame(this);
                    break;
                case GameStates.Win:
                    this._GameState = new WinGame(this);
                    break;
                case GameStates.Draw:
                    this._GameState = new DrawGame(this);
                    break;
                case GameStates.Lose:
                    this._GameState = new LoseGame(this);
                    break;
                default:
                    break;
            }
        }

        public bool IsPlaying()
        {
            if (this._GameState.GameStates != GameStates.Playing)
            {
                return false;
            }

            return true;
        }
    }
}