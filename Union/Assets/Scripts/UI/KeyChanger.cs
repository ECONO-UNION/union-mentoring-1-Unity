using InputSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Union.Services.UI
{
    public class KeyChanger : MonoBehaviour
    {
        [SerializeField]
        private Text _keyName;
        [SerializeField]
        private Text _codeName;

        private KeyName _name;
        private KeyCode _code;
        private Action<KeyChanger> _onClickCallback;

        public void Set(KeyName name, KeyCode code, Action<KeyChanger> onClickCallback)
        {
            _name = name;
            _code = code;
            _onClickCallback = onClickCallback;

            _keyName.text = _name.ToString();
            _codeName.text = _code.ToString();
        }

        public void OnClickChangeReady()
        {
            _codeName.text = "변경할 키를 눌러주세요";
            _onClickCallback?.Invoke(this);
        }

        public void ChangeKey(KeyCode keyCode)
        {
            _codeName.text = keyCode.ToString();
            InputSystem.InputManager.Instance.ChangeKey(_name, keyCode);
        }
    }
}