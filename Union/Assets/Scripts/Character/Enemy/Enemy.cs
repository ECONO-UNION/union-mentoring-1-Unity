using UnityEngine;

namespace Union.Services.Charcater.Enemy
{
    public class Enemy : Character
    {
        private FiniteStateMachine _finiteStateMachine;
        
        private void Awake()
        {
            this.BaseStat = new BaseStat();
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
            this.BaseStat.healthPoint.Set(20);

            this.BaseStat.physicalPower.Set(10);
            this.BaseStat.physicalDefense.Set(10);
            this.BaseStat.walkingSpeed.Set(10);
            this.BaseStat.runningSpeed.Set(20);
            this.BaseStat.jumpingPower.Set(10);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
            {
                return;
            }

            this.BaseStat.healthPoint.Decrease(10);
        }
    }
}