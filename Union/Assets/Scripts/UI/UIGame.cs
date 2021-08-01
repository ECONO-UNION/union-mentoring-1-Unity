using UnityEngine;
using UnityEngine.UI;

using Union.Services.Game;

namespace Union.Services.UI
{
    public class UIGame : Singleton<UIGame>
    {
        public Text playTimeText;
        public Text enemyCountText;

        private void Update()
        {
            SetPlayTimeText(GameLogic.Instance.gameTime.playTime.ToString("F2"));
        }

        private void SetPlayTimeText(string text)
        {
            this.playTimeText.text = text;
        }

        public void SetEnemyCountText(string text)
        {
            this.enemyCountText.text = text;
        }
    }
}