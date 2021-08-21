using UnityEngine;

namespace Union.Services.Charcater.Enemy
{
    public class Enemy : Character
    {
        private FiniteStateMachine _finiteStateMachine;
        
        private void Awake()
        {
            this.CharacterStat = new CharacterStat();
            this._finiteStateMachine = new FiniteStateMachine(this);
        }

        private void Start()
        {
            Initialize();
            this._finiteStateMachine.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachine.Run();
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