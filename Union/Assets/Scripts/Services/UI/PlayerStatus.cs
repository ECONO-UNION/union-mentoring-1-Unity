using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Union.Services.Charcater.Player;

namespace Union.Services.UI
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private Image _healthPointBar;

        [SerializeField]
        private Text _healthPointText;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateHealthPointUI();
        }

        private void Initialize()
        {
            this._healthPointBar.type = Image.Type.Filled;
            this._healthPointBar.fillMethod = Image.FillMethod.Horizontal;
            this._healthPointBar.fillOrigin = 0;
            this._healthPointBar.fillAmount = 100.0f;
        }

        private void UpdateHealthPointUI()
        {
            const float Test_HPBarFillAmountWeight = 10.0f;
            this._healthPointBar.fillAmount = Mathf.Lerp(this._healthPointBar.fillAmount, (float)this._player.BaseStat.HealthPoint.Get() / this._player.BaseStat.HealthPoint.GetMax(), Time.deltaTime * Test_HPBarFillAmountWeight);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("HP : ");
            stringBuilder.Append(this._player.BaseStat.HealthPoint.Get().ToString());
            this._healthPointText.text = stringBuilder.ToString();
        }
    }
}