using UnityEngine;

namespace Union.Services.Game
{
    public class GameLogic : Singleton<GameLogic>
    {
        private static class Constants
        {
            public const int BattleFieldEnemyCount = 10;
        }

        private EGameStatus _eGameStatus;

        public GameTime gameTime;

        private void Awake()
        {
            this.gameTime = new GameTime();
        }

        private void Start()
        {
            Initialize();
            StartGame();
        }

        private void Update()
        {
            if (IsGamePlaying() == true)
            {
                this.gameTime.UpdatePlayTime(Time.deltaTime);
            }
        }

        private void Initialize()
        {
            this._eGameStatus = EGameStatus.None;       
        }

        public void ReadyGame()
        {
            this._eGameStatus = EGameStatus.Ready;
        }

        public void StartGame()
        {
            this._eGameStatus = EGameStatus.InGame;

        }

        public void PauseGame()
        {
            this._eGameStatus = EGameStatus.Pause;
        }

        public void ResumeGame()
        {
            this._eGameStatus = EGameStatus.InGame;
        }

        public void WinGame()
        {
            this._eGameStatus = EGameStatus.Win;
        }

        public void LoseGame()
        {
            this._eGameStatus = EGameStatus.Lose;
        }

        private bool IsGamePlaying()
        {
            if (this._eGameStatus != EGameStatus.InGame)
            {
                return false;
            }

            return true;
        }
    }
}