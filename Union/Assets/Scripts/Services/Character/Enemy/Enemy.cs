using UnityEngine;
using Union.Util.Csv;

namespace Union.Services.Charcater.Enemy
{
    public class Enemy : Character
    {
        [SerializeField]
        private int _infoID = 1001;

        private FiniteStateMachineController _finiteStateMachineController;
        
        private void Awake()
        {
            EnemyInformation enemyInformation = Storage<EnemyInformation>.Instance.GetData(this._infoID);

            this.BaseStat = new BaseStat(enemyInformation.HealthPoint,
                                        enemyInformation.PhysicalPower, enemyInformation.PhysicalDefense,
                                        enemyInformation.WalkingSpeed, enemyInformation.RunningSpeed,
                                        enemyInformation.JumpingPower);
        }

        private void Start()
        {
            this._finiteStateMachineController = new FiniteStateMachineController(this);
            this._finiteStateMachineController.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachineController.Run();

            if (this._finiteStateMachineController.CurrentState == StateNumber.Dead)
            {
                this.gameObject.SetActive(false);
                this.enabled = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
                return;

            this.BaseStat.HealthPoint.Decrease(10);
        }
    }
}