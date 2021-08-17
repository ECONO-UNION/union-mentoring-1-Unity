namespace Union.Services.Game
{
    public enum GameStates
    {
        Ready,
        Start,
        Playing,
        Pause,
        Win,
        Draw,
        Lose,
    }

    public abstract class GameState
    {
        protected GameLogic GameLogic { get; set; }
        public GameStates GameStates { get; protected set; } // TO DO : Dictionary 구조로 인해 필요가 없어짐, 추후 정리 필요

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class ReadyGame : GameState
    {
        public ReadyGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Ready;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }

    public class StartGame : GameState
    {
        public StartGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Start;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }

    public class PlayingGame : GameState
    {
        public PlayingGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Playing;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {
            // TO DO : Lose & Draw 로직 고민 필요 (Player 상태 가져오기)
            if (BattleField.Instance.currentEnemyCount <= 0)
            {
                this.GameLogic.SetState(GameStates.Win);
                UnityEngine.Debug.Log("게임 승리");
            }
        }

        public override void Exit()
        {

        }
    }

    public class PauseGame : GameState
    {
        public PauseGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Pause;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }

    public class WinGame : GameState
    {
        public WinGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Win;
            this.GameLogic = gameLogic;
        }
        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }

    public class DrawGame : GameState
    {
        public DrawGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Lose;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }

    public class LoseGame : GameState
    {
        public LoseGame(GameLogic gameLogic)
        {
            this.GameStates = GameStates.Lose;
            this.GameLogic = gameLogic;
        }

        public override void Enter()
        {

        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }
}