using UnityEngine;

namespace Union.Services.Unit
{
    public class Enemy : MonoBehaviour
    {
        private UnitStat _unitStat;
        private EnemyState _enemyState;
        
        private void Awake()
        {
            this._unitStat = new UnitStat();
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
            this._unitStat.healthPoint.LateUpdateHeadUpDisplay();
        }

        private void Initialize()
        {
            this._unitStat.healthPoint.SetHeadUpDisplay(this.gameObject);
            this._unitStat.healthPoint.Set(20);

            this._unitStat.physicalPower.Set(10);
            this._unitStat.physicalDefense.Set(10);
            this._unitStat.walkingSpeed.Set(10);
            this._unitStat.runningSpeed.Set(20);
            this._unitStat.jumpingPower.Set(10);
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
                    this._enemyState = new AliveEnemy(this, this._unitStat.healthPoint);
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

            this._unitStat.healthPoint.Decrease(10);
        }
    }
}