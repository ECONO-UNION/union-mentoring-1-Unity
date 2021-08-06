using UnityEngine;
using Union.Services.UI;

namespace Union.Services.Game
{
    public class BattleField : Singleton<BattleField>
    {
        private static class Constants
        {
            public const int InitialEnemyCount = 10;
        }

        public int totalEnemyCount { get; private set; }
        public int currentEnemyCount { get; private set; }
        public int deadEnemyCount
        {
            get
            {
                return this.totalEnemyCount - currentEnemyCount;
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.totalEnemyCount = Constants.InitialEnemyCount;
            this.currentEnemyCount = totalEnemyCount;
        }

        public void DecreaseEnemyCount(int amount)
        {
            this.currentEnemyCount -= amount;
        }
    }
}