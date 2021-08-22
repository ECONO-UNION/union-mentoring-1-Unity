using InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Union.Services.UI.KeyChange
{
    public class KeyChanger : MonoBehaviour
    {
        [SerializeField]
        private KeySetting _keySetting;
        [SerializeField]
        private EventSystem _eventSystem;

        private int _selectedKeyIndex;
        private bool _isChangeState;

        public void ClickChangeToReady(int index)
        {
            _selectedKeyIndex = index;
            _eventSystem.enabled = false;
            _isChangeState = true;
        }

        private void OnGUI()
        {
            if (!_isChangeState)
                return;

            KeyCode pressedKey = GetPressedKey();
            if (pressedKey == KeyCode.None)
                return;

            ChangeKey(pressedKey);
        }

        private KeyCode GetPressedKey()
        {
            KeyCode pressedKey = KeyCode.None;
            Event currentEvent = Event.current;

            if (currentEvent.isKey)
            {
                pressedKey = currentEvent.keyCode;
            }
            else if (currentEvent.isMouse)
            {
                if (currentEvent.button == 0)
                    pressedKey = KeyCode.Mouse0;
                if (currentEvent.button == 1)
                    pressedKey = KeyCode.Mouse1;
                if (currentEvent.button == 2)
                    pressedKey = KeyCode.Mouse2;
            }
            return pressedKey;
        }

        private void ChangeKey(KeyCode pressedKey)
        {
            _eventSystem.enabled = true;
            _isChangeState = false;

            _keySetting.ChangeKeyMapElement(_selectedKeyIndex, pressedKey);
            KeyName keyName = _keySetting.GetElementKeyName(_selectedKeyIndex);
            InputSystem.InputManager.Instance.ChangeKey(keyName, pressedKey);
        }
    }
}