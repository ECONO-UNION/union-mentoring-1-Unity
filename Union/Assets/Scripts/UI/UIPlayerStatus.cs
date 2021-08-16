using UnityEngine;
using UnityEngine.UI;

using Union.Services.Unit;

namespace Union.Services.UI
{
    public class UIPlayerStatus : MonoBehaviour
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
            this._healthPointBar.fillAmount = Mathf.Lerp(this._healthPointBar.fillAmount, (float)this._player.UnitStat.healthPoint.Get() / this._player.UnitStat.healthPoint.GetMax(), Time.deltaTime * Test_HPBarFillAmountWeight);
            this._healthPointText.text = "HP : " + this._player.UnitStat.healthPoint.Get().ToString();
        }
    }
}