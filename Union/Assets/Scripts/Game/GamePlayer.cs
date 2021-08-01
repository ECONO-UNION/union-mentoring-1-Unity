using System.Collections;
using UnityEngine;

using Union.Services.UI;

namespace Union.Services.Game
{
    public class GamePlayer : MonoBehaviour
    {
        public UIGamePlayerStatus uiGamePlayerStatus;
        private UnitAbility _unitAbility;

        private void Awake()
        {
            this._unitAbility = new UnitAbility();
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

            this.uiGamePlayerStatus.SetHealthPointUI(this._unitAbility.healthPoint.Get());
        }

        private void DecreaseHealthPoint(int amount)
        {
            this._unitAbility.healthPoint.Decrease(amount);
            this.uiGamePlayerStatus.SetHealthPointUI(this._unitAbility.healthPoint.Get());
        }


        private void OnCollisionEnter(Collision collision)
        {
            BattleField.Instance.DecreaseEnemyCount(1); // TO DO : enemy로 주체 이동 필요
            DecreaseHealthPoint(10);
            
        }
    }
}