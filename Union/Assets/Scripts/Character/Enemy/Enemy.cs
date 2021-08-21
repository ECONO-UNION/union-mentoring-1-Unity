using UnityEngine;

namespace Union.Services.Charcater.Enemy
{
    public class Enemy : Character
    {
        private EnemyFiniteStateMachine _enemyFiniteStateMachine;
        
        private void Awake()
        {
            this.CharacterStat = new CharacterStat();
            this._enemyFiniteStateMachine = new EnemyFiniteStateMachine(this);
        }

        private void Start()
        {
            Initialize();
            this._enemyFiniteStateMachine.Initialize();
        }

        private void Update()
        {
            this._enemyFiniteStateMachine.Run();
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