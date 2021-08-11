using System.Collections;
using UnityEngine;

namespace Union.Services.Unit
{
    public class Player : MonoBehaviour
    {
        public UnitStat UnitStat { private set; get; }
        private PlayerState _playerState;

        private void Awake()
        {
            this.UnitStat = new UnitStat();
        }

        private void Start()
        {
            Initialize();
            SetState(PlayerStates.Alive);
        }

        private void Update()
        {
            this._playerState.Run();
        }

        private void LateUpdate()
        {
            this.UnitStat.healthPoint.LateUpdateHeadUpDisplay();
        }

        private void Initialize()
        {
            this.UnitStat.healthPoint.SetHeadUpDisplay(this.gameObject);
            this.UnitStat.healthPoint.Set(100);

            this.UnitStat.physicalPower.Set(10);
            this.UnitStat.physicalDefense.Set(10);
            this.UnitStat.walkingSpeed.Set(10);
            this.UnitStat.runningSpeed.Set(20);
            this.UnitStat.jumpingPower.Set(10);
        }

        public void SetState(PlayerStates playerStates)
        {
            this._playerState?.Exit();
            CreateIUnitState(playerStates);
            this._playerState?.Enter();
        }

        private void CreateIUnitState(PlayerStates playerStates)
        {
            switch (playerStates)
            {
                case PlayerStates.Alive:
                    this._playerState = new AlivePlayer(this, this.UnitStat.healthPoint);
                    break;
                case PlayerStates.Dead:
                    this._playerState = new DeadPlayer();
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

            this.UnitStat.healthPoint.Decrease(10);
        }
    }
}