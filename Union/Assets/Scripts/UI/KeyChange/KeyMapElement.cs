using InputSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Union.Services.UI.KeyChange
{
    [System.Serializable]
    public class KeyMapElement
    {
        [SerializeField]
        private KeyName _keyName;
        private KeyCode _keyCode;

        [SerializeField]
        private Text _keyCodeText;

        public KeyName KeyName { get => _keyName; }
        public KeyCode KeyCode
        {
            set
            {
                _keyCode = value;
                _keyCodeText.text = value.ToString();
            }
        }
    }
}