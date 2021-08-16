using UnityEngine;
using UnityEngine.UI;

namespace Union.Services.Stat
{
    public class HealthPoint : Stat
    {
        private GameObject _headUpDisplayCanavs;
        private Image _headUpDispalyHealthPointBar;

        public void SetHeadUpDisplay(GameObject obj)
        {
            this._headUpDisplayCanavs = obj.transform.Find("HeadUpDisplayCanvas").gameObject;
            this._headUpDispalyHealthPointBar = this._headUpDisplayCanavs.transform.Find("HealthPointBar").GetComponent<Image>();
        }

        public void LateUpdateHeadUpDisplay()
        {
            this._headUpDispalyHealthPointBar.fillAmount = Mathf.Lerp(this._headUpDispalyHealthPointBar.fillAmount, (float)this._amount / this._maxAmount, Time.deltaTime * 10.0f);
            LateUpdateHeadUpDisplayHealthBarLookAt();
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