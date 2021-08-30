using UnityEngine;
using UnityEngine.UI;

using Union.Services.Game;

namespace Union.Services.UI
{
    public class GameStatus : Singleton<GameStatus>
    {
        [SerializeField]
        private Text _playTimeText;

        [SerializeField]
        private Text _enemyCountText;

        private void Update()
        {
            UpdatePlayTimeUI();
            UpdateEnemyCountUI();
        }

        private void UpdatePlayTimeUI()
        {
            this._playTimeText.text = Logic.Instance.Time.playTime.ToString("F2");
        }

        private void UpdateEnemyCountUI()
        {
            string enemyCountString = BattleField.Instance.currentEnemyCount.ToString() + " / " + BattleField.Instance.totalEnemyCount.ToString();
            this._enemyCountText.text = enemyCountString;
        }
    }
}