using System.Collections;
using UnityEngine;

using Union.Services.UI;
using Union.Services.Stat;

namespace Union.Services.Game
{
    public class GamePlayer : MonoBehaviour
    {
        private UnitStat _unitAbility;

        public UnitStat UnitStat
        {
            get
            {
                return this._unitAbility;
            }
        }

        private void Awake()
        {
            this._unitAbility = new UnitStat();
        }

        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            this._unitAbility.healthPoint.Set(100);
            this._unitAbility.physicalPower.Set(10);
            this._unitAbility.physicalDefense.Set(10);
            this._unitAbility.walkingSpeed.Set(10);
            this._unitAbility.runningSpeed.Set(20);
            this._unitAbility.jumpingPower.Set(10);
        }

        private void OnCollisionEnter(Collision collision)
        {
            BattleField.Instance.DecreaseEnemyCount(1); // TO DO : enemy로 주체 이동 및 이벤트로 변경
            this._unitAbility.healthPoint.Decrease(10);
        }
    }
}