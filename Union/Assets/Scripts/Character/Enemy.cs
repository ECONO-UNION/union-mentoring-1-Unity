using UnityEngine;

namespace Union.Services.Charcater
{
    public class Enemy : MonoBehaviour
    {
        private CharacterStat _characterStat;
        private EnemyState _enemyState;
        
        private void Awake()
        {
            this._characterStat = new CharacterStat();
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

        private void LateUpdate()
        {
            this._characterStat.healthPoint.LateUpdateHeadUpDisplay();
        }

        private void Initialize()
        {
            this._characterStat.healthPoint.SetHeadUpDisplay(this.gameObject);
            this._characterStat.healthPoint.Set(20);

            this._characterStat.physicalPower.Set(10);
            this._characterStat.physicalDefense.Set(10);
            this._characterStat.walkingSpeed.Set(10);
            this._characterStat.runningSpeed.Set(20);
            this._characterStat.jumpingPower.Set(10);
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
                    this._enemyState = new AliveEnemy(this, this._characterStat.healthPoint);
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

            this._characterStat.healthPoint.Decrease(10);
        }
    }
}