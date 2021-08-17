using System.Collections.Generic;
using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        private Dictionary<GameStates, GameState> _gameStates;
        private GameState _currentGameState;

        public GameTime gameTime;

        private void Awake()
        {
            this.gameTime = new GameTime();
        }

        private void Start()
        {
            Initialize();
            SetState(GameStates.Playing);
        }

        private void Update()
        {
            if (IsPlaying() == true)
            {
                this.gameTime.UpdatePlayTime(Time.deltaTime);
            }

            this._currentGameState.Run();
        }

        private void Initialize()
        {
            this._gameStates = new Dictionary<GameStates, GameState>();

            this._gameStates.Add(GameStates.Ready, new ReadyGame(this));
            this._gameStates.Add(GameStates.Start, new StartGame(this));
            this._gameStates.Add(GameStates.Playing, new PlayingGame(this));
            this._gameStates.Add(GameStates.Pause, new PauseGame(this));
            this._gameStates.Add(GameStates.Win, new WinGame(this));
            this._gameStates.Add(GameStates.Draw, new DrawGame(this));
            this._gameStates.Add(GameStates.Lose, new LoseGame(this));
        }

        public void SetState(GameStates gameStates)
        {
            this._currentGameState?.Exit();
            this._currentGameState = GetGameState(gameStates);
            this._currentGameState?.Enter();
        }

        private GameState GetGameState(GameStates gameStates)
        {
            if (this._gameStates.ContainsKey(gameStates) == false)
                return null;

            return this._gameStates[gameStates];
        }

        public bool IsPlaying()
        {
            if (this._currentGameState.GameStates != GameStates.Playing)
            {
                return false;
            }

            return true;
        }
    }
}