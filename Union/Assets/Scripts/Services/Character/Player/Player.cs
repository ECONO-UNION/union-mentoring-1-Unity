using UnityEngine;

namespace Union.Services.Charcater.Player
{
    public class Player : Character
    {
        private FiniteStateMachineController _finiteStateMachineController;

        private void Awake()
        {
            this.BaseStat = new BaseStat(100, 10, 10, 10, 20, 10);
            this._finiteStateMachineController = new FiniteStateMachineController(this);
        }

        private void Start()
        {
            this._finiteStateMachineController.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachineController.Run();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")            
                return;

            this.BaseStat.HealthPoint.Decrease(10);
        }
    }
}