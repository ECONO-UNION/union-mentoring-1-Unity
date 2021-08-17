using UnityEngine.EventSystems;
using UnityEngine;

namespace Union.Services.UI
{
    public class PageKeyChange : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private KeyChanger _keyChangerPrefab;
        private KeyChanger _currentKeyChanger;

        private void Start()
        {
            var keyInputs = InputSystem.InputManager.Instance.KeyInputs;
            foreach (var input in keyInputs)
            {
                var keyChanger = Instantiate(_keyChangerPrefab, transform);
                keyChanger.Set(input.Key, input.Value.Code, ExecuteKeyChanger);
            }
        }

        private void ExecuteKeyChanger(KeyChanger keyChanger)
        {
            _currentKeyChanger = keyChanger;
            _eventSystem.enabled = false;
        }

        private void OnGUI()
        {
            if (_currentKeyChanger == null)
                return;

            KeyCode pressedKey = GetPressedKey();
            if (pressedKey == KeyCode.None)
                return;

            _currentKeyChanger.ChangeKey(pressedKey);
            _eventSystem.enabled = true;
            _currentKeyChanger = null;
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
    }
}