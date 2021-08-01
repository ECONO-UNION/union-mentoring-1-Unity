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

        private Dictionary<InputAxisName, AxisKey> _axisKeys = new Dictionary<InputAxisName, AxisKey>();
        private Dictionary<InputButtonName, ButtonKey> _buttonKeys = new Dictionary<InputButtonName, ButtonKey>();

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
                _axisKeys[(InputAxisName)keyType] = new AxisKey(keyType.ToString());
            }
        }

        private void BindButtonKeys()
        {
            var keyTypes = Enum.GetValues(typeof(InputButtonName));
            foreach (var keyType in keyTypes)
            {
                _buttonKeys[(InputButtonName)keyType] = new ButtonKey(keyType.ToString());
            }
        }

        private void Update()
        {
            foreach (var key in _axisKeys.Values)
            {
                key.Value = Input.GetAxis(key.Name);
            }

            foreach (var key in _buttonKeys.Values)
            {
                key.IsButtonPressed = Input.GetButton(key.Name);
                key.IsButtonDown = Input.GetButtonDown(key.Name);
                key.IsButtonUp = Input.GetButtonUp(key.Name);
            }
        }

        public AxisKey GetAxisKey(InputAxisName type)
        {
            if (!_axisKeys.ContainsKey(type))
            {
                _axisKeys[type] = new AxisKey(type.ToString());
            }
            return _axisKeys[type];
        }

        public ButtonKey GetButtonKey(InputButtonName type)
        {
            if (!_buttonKeys.ContainsKey(type))
            {
                _buttonKeys[type] = new ButtonKey(type.ToString());
            }
            return _buttonKeys[type];
        }
    }
}