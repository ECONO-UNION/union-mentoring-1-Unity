namespace Union.Services.Charcater
{
    public enum EnemyStates
    {
        Alive,
        Dead,
    }

    public abstract class EnemyState
    {
        protected Enemy Enemy { get; set; }
        public EnemyStates EnemyStates { get; protected set; }

        public abstract void Enter();
        public abstract void Run();
        public abstract void Exit();
    }

    public class AliveEnemy : EnemyState
    {
        public Union.Services.Stat.Stat HealthPoint { private get; set; }

        public AliveEnemy(Enemy enemy, Union.Services.Stat.Stat healthPoint)
        {
            this.EnemyStates = EnemyStates.Alive;
            this.Enemy = enemy;
            this.HealthPoint = healthPoint;
        }

        public override void Enter()
        {
            
        }

        public override void Run()
        {
            if (this.HealthPoint.Get() <= 0)
            {
                this.Enemy.SetState(EnemyStates.Dead);
            }
        }

        public override void Exit()
        {

        }
    }

    public class DeadEnemy : EnemyState
    {
        public DeadEnemy(Enemy enemy)
        {
            this.EnemyStates = EnemyStates.Dead;
            this.Enemy = enemy;
        }

        public override void Enter()
        {
            Union.Services.Game.BattleField.Instance.DecreaseEnemyCount(1);
            this.Enemy.enabled = false;
            this.Enemy.gameObject.SetActive(false);
        }

        public override void Run()
        {

        }

        public override void Exit()
        {

        }
    }
}
