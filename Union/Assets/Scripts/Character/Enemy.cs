using UnityEngine;

namespace Union.Services.Charcater
{
    public class Enemy : Character
    {
        private EnemyState _enemyState;
        
        private void Awake()
        {
            this.CharacterStat = new CharacterStat();
        }

        private void Start()
        {
            Initialize();
            SetState(EnemyStates.Alive);
        }

        private void Update()
        {
            this._enemyState.Run();
        }

        private void Initialize()
        {
            this.CharacterStat.healthPoint.Set(20);

            this.CharacterStat.physicalPower.Set(10);
            this.CharacterStat.physicalDefense.Set(10);
            this.CharacterStat.walkingSpeed.Set(10);
            this.CharacterStat.runningSpeed.Set(20);
            this.CharacterStat.jumpingPower.Set(10);
        }

        public void SetState(EnemyStates enemyStates)
        {
            this._enemyState?.Exit();
            CreateEnemyState(enemyStates);
            this._enemyState?.Enter();
        }

        private void CreateEnemyState(EnemyStates enemyStates)
        {
            switch (enemyStates)
            {
                case EnemyStates.Alive:
                    this._enemyState = new AliveEnemy(this, this.CharacterStat.healthPoint);
                    break;
                case EnemyStates.Dead:
                    this._enemyState = new DeadEnemy(this);
                    break;
                default:
                    break;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
            {
                return;
            }

            this.CharacterStat.healthPoint.Decrease(10);
        }
    }
}