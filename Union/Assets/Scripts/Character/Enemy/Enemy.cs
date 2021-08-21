using UnityEngine;

namespace Union.Services.Charcater.Enemy
{
    public class Enemy : Character
    {
        private FiniteStateMachine _finiteStateMachine;
        
        private void Awake()
        {
            this.BaseStat = new BaseStat(20, 10, 10, 10, 20, 10);
            this._finiteStateMachine = new FiniteStateMachine(this);
        }

        private void Start()
        {
            this._finiteStateMachine.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachine.Run();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
                return;

            this.BaseStat.HealthPoint.Decrease(10);
        }
    }
}