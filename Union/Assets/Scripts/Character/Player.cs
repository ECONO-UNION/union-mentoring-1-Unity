using System.Collections;
using UnityEngine;

namespace Union.Services.Charcater
{
    public class Player : MonoBehaviour
    {
        public CharacterStat CharacterStat { private set; get; }
        private PlayerState _playerState;

        private void Awake()
        {
            this.CharacterStat = new CharacterStat();
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
            this.CharacterStat.healthPoint.LateUpdateHeadUpDisplay();
        }

        private void Initialize()
        {
            this.CharacterStat.healthPoint.SetHeadUpDisplay(this.gameObject);
            this.CharacterStat.healthPoint.Set(100);

            this.CharacterStat.physicalPower.Set(10);
            this.CharacterStat.physicalDefense.Set(10);
            this.CharacterStat.walkingSpeed.Set(10);
            this.CharacterStat.runningSpeed.Set(20);
            this.CharacterStat.jumpingPower.Set(10);
        }

        public void SetState(PlayerStates playerStates)
        {
            this._playerState?.Exit();
            CreateIPlayerState(playerStates);
            this._playerState?.Enter();
        }

        private void CreateIPlayerState(PlayerStates playerStates)
        {
            switch (playerStates)
            {
                case PlayerStates.Alive:
                    this._playerState = new AlivePlayer(this, this.CharacterStat.healthPoint);
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

            this.CharacterStat.healthPoint.Decrease(10);
        }
    }
}