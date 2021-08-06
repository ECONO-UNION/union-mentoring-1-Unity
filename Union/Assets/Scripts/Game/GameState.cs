namespace Union.Services.Game
{
    public enum GameState
    {
        ReadyGame,
        StartGame,
        PlayingGame,
        PauseGame,
        WinGame,
        LoseGame,
    }

    public interface IGameState
    {
        void Enter();
        void Run();
        void Exit();
    }

    public class ReadyGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }

    public class StartGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }

    public class PlayingGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }

    public class PauseGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }

    public class WinGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }

    public class LoseGame : IGameState
    {
        public void Enter()
        {

        }

        public void Run()
        {

        }

        public void Exit()
        {

        }
    }
}