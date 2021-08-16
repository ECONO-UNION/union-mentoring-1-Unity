using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        private GameState _gameState;

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

            this._gameState.Run();
        }

        public void SetState(GameStates gameStates)
        {
            this._gameState?.Exit();
            CreateIGameState(gameStates);
            this._gameState.Enter();
        }

        private void CreateIGameState(GameStates gameStates)
        {
            switch (gameStates)
            {
                case GameStates.Ready:
                    this._gameState = new ReadyGame(this);
                    break;
                case GameStates.Start:
                    this._gameState = new StartGame(this);
                    break;
                case GameStates.Playing:
                    this._gameState = new PlayingGame(this);
                    break;
                case GameStates.Pause:
                    this._gameState = new PauseGame(this);
                    break;
                case GameStates.Win:
                    this._gameState = new WinGame(this);
                    break;
                case GameStates.Draw:
                    this._gameState = new DrawGame(this);
                    break;
                case GameStates.Lose:
                    this._gameState = new LoseGame(this);
                    break;
                default:
                    break;
            }
        }

        public bool IsPlaying()
        {
            if (this._gameState.GameStates != GameStates.Playing)
            {
                return false;
            }

            return true;
        }
    }
}