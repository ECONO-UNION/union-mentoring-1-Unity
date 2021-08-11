namespace Union.Services.Unit
{
    public enum PlayerStates
    {
        Alive,
        Dead,
    }

    public abstract class PlayerState
    {
        protected Player Player { get; set; }
        public PlayerStates PlayerStates { get; protected set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class AlivePlayer : PlayerState
    {
        public Union.Services.Stat.HealthPoint HealthPoint { private get; set; }

        public AlivePlayer(Player player, Union.Services.Stat.HealthPoint healthPoint)
        {
            this.PlayerStates = PlayerStates.Alive;
            this.Player = player;
            this.HealthPoint = healthPoint;
        }

        public override void Enter()
        {
            
        }

        public override void Run()
        {
            if (this.HealthPoint.Get() <= 0)
            {
                this.Player.SetState(PlayerStates.Dead);
            }
        }

        public override void Exit()
        {

        }
    }

    public class DeadPlayer : PlayerState
    {
        public DeadPlayer()
        {
            this.PlayerStates = PlayerStates.Dead;
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("Player Dead");
        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }
}
