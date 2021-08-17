using UnityEngine;
using UnityEngine.UI;

using Union.Services.Charcater;

namespace Union.Services.UI
{
    public class CharacterHealthPointHeadUpDisplay : MonoBehaviour
    {
        private Character _character;
        private CharacterStat _characterStat;

        private GameObject _headUpDisplayCanavs;
        private Image _headUpDispalyHealthPointBar;

        private void Start()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            LateUpdateHeadUpDisplay();
            LateUpdateHeadUpDisplayHealthBarLookAt();
        }

        public void Initialize()
        {
            this._character = this.gameObject.GetComponent<Character>();
            this._characterStat = this._character.CharacterStat;

            this._headUpDisplayCanavs = this.gameObject.transform.Find("HeadUpDisplayCanvas").gameObject;
            this._headUpDispalyHealthPointBar = this._headUpDisplayCanavs.transform.Find("HealthPointBar").GetComponent<Image>();
        }

        public void LateUpdateHeadUpDisplay()
        {
            const float Test_HPBarFillAmountWeight = 10.0f;
            this._headUpDispalyHealthPointBar.fillAmount = Mathf.Lerp(this._headUpDispalyHealthPointBar.fillAmount,
                                                                      (float)this._characterStat.healthPoint.Get() / this._characterStat.healthPoint.GetMax(),
                                                                      Time.deltaTime * Test_HPBarFillAmountWeight);
        }

        private void LateUpdateHeadUpDisplayHealthBarLookAt()
        {
            if (this._headUpDisplayCanavs == null)
            {
                return;
            }

            Vector3 worldPosition = this._headUpDisplayCanavs.transform.position + Camera.main.transform.rotation * Vector3.forward;
            Vector3 worldUp = Camera.main.transform.rotation * Vector3.up;

            this._headUpDisplayCanavs.transform.LookAt(worldPosition, worldUp);
        }
    }
}