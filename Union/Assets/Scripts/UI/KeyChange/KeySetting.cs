using UnityEngine;
using InputSystem;
using System.Collections.Generic;

namespace Union.Services.UI.KeyChange
{
    public class KeySetting : MonoBehaviour
    {
        [SerializeField]
        private List<KeyMapElement> _keyMapElements;

        private void Start()
        {
            foreach (var element in _keyMapElements)
            {
                KeyCode keyCode = InputSystem.InputManager.Instance.KeyInputs[element.KeyName].Code;
                element.KeyCode = keyCode;
            }
        }

        public void ChangeKeyMapElement(int index, KeyCode keyCode)
        {
            _keyMapElements[index].KeyCode = keyCode;
        }

        public KeyName GetElementKeyName(int index)
        {
            return _keyMapElements[index].KeyName;
        }
    }
}