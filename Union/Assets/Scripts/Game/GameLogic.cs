using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        private static class Constants
        {
            public const int BattleFieldEnemyCount = 10;
        }

        private IGameState _iGameState;
        private GameState _gameState;

        public GameTime gameTime;

        private void Awake()
        {
            this.gameTime = new GameTime();
        }

        private void Start()
        {
            SetGameState(GameState.ReadyGame);
        }

        private void Update()
        {
            if (IsGamePlaying() == true)
            {
                this.gameTime.UpdatePlayTime(Time.deltaTime);
            }

            this._iGameState.Run();
        }

        public void SetGameState(GameState gameState)
        {
            this._gameState = gameState;
            this._iGameState.Exit();
            CreateIGameState(gameState);
            this._iGameState.Enter();
        }

        private void CreateIGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.ReadyGame:
                    this._iGameState = new ReadyGame();
                    break;
                case GameState.StartGame:
                    this._iGameState = new StartGame();
                    break;
                case GameState.PlayingGame:
                    this._iGameState = new PlayingGame();
                    break;
                case GameState.PauseGame:
                    this._iGameState = new PauseGame();
                    break;
                case GameState.WinGame:
                    this._iGameState = new WinGame();
                    break;
                case GameState.LoseGame:
                    this._iGameState = new LoseGame();
                    break;
                default:
                    break;
            }
        }

        private bool IsGamePlaying()
        {
            if (this._gameState != GameState.StartGame)
            {
                return false;
            }

            return true;
        }
    }
}