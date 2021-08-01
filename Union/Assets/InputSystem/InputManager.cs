using System;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InputManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(InputManager).Name);
                        _instance = obj.AddComponent<InputManager>();
                    }
                    return _instance;
                }
                return _instance;
            }
        }

        private Dictionary<InputAxisName, InputAxisEvent> _axisKeys = new Dictionary<InputAxisName, InputAxisEvent>();
        private Dictionary<InputButtonName, InputButtonEvent> _buttonKeys = new Dictionary<InputButtonName, InputButtonEvent>();

        public bool IsInputBlock { get; set; }

        private void Awake()
        {
            BindButtonKeys();
            BindAxisKeys();
        }
        private void BindAxisKeys()
        {
            var keyTypes = Enum.GetValues(typeof(InputAxisName));
            foreach (var keyType in keyTypes)
            {
                _axisKeys[(InputAxisName)keyType] = new InputAxisEvent(keyType.ToString());
            }
        }

        private void BindButtonKeys()
        {
            var keyTypes = Enum.GetValues(typeof(InputButtonName));
            foreach (var keyType in keyTypes)
            {
                _buttonKeys[(InputButtonName)keyType] = new InputButtonEvent(keyType.ToString());
            }
        }

        private void Update()
        {
            if (IsInputBlock)
            {
                return;
            }

            foreach (var key in _axisKeys.Values)
            {
                key.onPressedKey?.Invoke(Input.GetAxis(key.Name));
            }

            foreach (var key in _buttonKeys.Values)
            {
                if (Input.GetButtonDown(key.Name))
                {
                    key.onDownKey?.Invoke();
                }
                else if (Input.GetButton(key.Name))
                {
                    key.onPressedKey?.Invoke();
                }
                else if (Input.GetButtonUp(key.Name))
                {
                    key.onUpKey?.Invoke();
                }
            }
        }

        public InputAxisEvent GetAxisKey(InputAxisName type)
        {
            if (!_axisKeys.ContainsKey(type))
            {
                _axisKeys[type] = new InputAxisEvent(type.ToString());
            }
            return _axisKeys[type];
        }

        public InputButtonEvent GetButtonKey(InputButtonName type)
        {
            if (!_buttonKeys.ContainsKey(type))
            {
                _buttonKeys[type] = new InputButtonEvent(type.ToString());
            }
            return _buttonKeys[type];
        }
    }
}