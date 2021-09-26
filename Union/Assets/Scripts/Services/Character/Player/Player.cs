using UnityEngine;
using Union.Util.Csv;

namespace Union.Services.Charcater.Player
{
    public class Player : Character
    {
        [SerializeField]
        private int _infoID = 1001;

        private FiniteStateMachineController _finiteStateMachineController;

        private void Awake()
        {
            PlayerInformation playerInformation = Storage<PlayerInformation>.Instance.GetData(this._infoID);

            this.BaseStat = new BaseStat(playerInformation.HealthPoint,
                                        playerInformation.PhysicalPower, playerInformation.PhysicalDefense,
                                        playerInformation.WalkingSpeed, playerInformation.RunningSpeed,
                                        playerInformation.JumpingPower);
        }

        private void Start()
        {
            this._finiteStateMachineController = new FiniteStateMachineController(this);
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