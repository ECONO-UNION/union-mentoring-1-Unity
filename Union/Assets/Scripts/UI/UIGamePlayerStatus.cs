using UnityEngine;
using UnityEngine.UI;

using Union.Services.Game;

namespace Union.Services.UI
{
    public class UIGamePlayerStatus : MonoBehaviour
    {
        [SerializeField]
        private GamePlayer _gamePlayer;

        [SerializeField]
        private Image _healthPointImage;

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
            this._healthPointImage.type = Image.Type.Filled;
            this._healthPointImage.fillMethod = Image.FillMethod.Horizontal;
            this._healthPointImage.fillOrigin = 0;
            this._healthPointImage.fillAmount = 100.0f;
        }

        private void UpdateHealthPointUI()
        {
            this._healthPointImage.fillAmount = this._gamePlayer.UnitStat.healthPoint.Get() / 100.0f;
            this._healthPointText.text = "HP : " + this._gamePlayer.UnitStat.healthPoint.Get().ToString();
        }
    }
}