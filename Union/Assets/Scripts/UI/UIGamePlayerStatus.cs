using UnityEngine;
using UnityEngine.UI;

namespace Union.Services.UI
{
    public class UIGamePlayerStatus : MonoBehaviour
    {
        public Image healthPointImage;
        public Text healthPointText;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.healthPointImage.type = Image.Type.Filled;
            this.healthPointImage.fillMethod = Image.FillMethod.Horizontal;
            this.healthPointImage.fillOrigin = 0;
            this.healthPointImage.fillAmount = 100.0f;
        }

        public void SetHealthPointUI(int healthPoint)
        {
            this.healthPointImage.fillAmount = healthPoint / 100.0f;
            this.healthPointText.text = "HP : " + healthPoint.ToString();
        }
    }
}