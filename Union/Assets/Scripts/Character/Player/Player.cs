using UnityEngine;

namespace Union.Services.Charcater.Player
{
    public class Player : Character
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
            this.BaseStat.HealthPoint.Set(100);

            this.BaseStat.PhysicalPower.Set(10);
            this.BaseStat.PhysicalDefense.Set(10);
            this.BaseStat.WalkingSpeed.Set(10);
            this.BaseStat.RunningSpeed.Set(20);
            this.BaseStat.JumpingPower.Set(10);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")            
                return;

            this.BaseStat.HealthPoint.Decrease(10);
        }
    }
}