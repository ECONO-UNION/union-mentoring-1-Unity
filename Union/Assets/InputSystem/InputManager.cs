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

        private Dictionary<KeyName, KeyInput> _keyInputs = new Dictionary<KeyName, KeyInput>();
        private Dictionary<MouseName, MouseInput> _mouseInputs = new Dictionary<MouseName, MouseInput>();
        private InputSetting _inputSetting;

        public Vector2 MousePosition { get; private set; }

        private void Awake()
        {
            _inputSetting = Resources.Load<InputSetting>(InputSystemSetting.Path);
            if (_inputSetting == null)
            {
                Debug.LogError("Input System을 설정하지 않았습니다");
                return;
            }
            BindButtonKeys();
            BindMouseKeys();
        }

        private void BindButtonKeys()
        {
            foreach (var keyButton in _inputSetting.KeyButtons)
            {
                KeyName name = EnumMapper.GetEnumType<KeyName>(keyButton.name);
                _keyInputs[name] = new KeyInput(keyButton.button);
            }
        }

        private void BindMouseKeys()
        {
            var keyTypes = Enum.GetValues(typeof(MouseName));
            foreach (var keyType in keyTypes)
            {
                _mouseInputs[(MouseName)keyType] = new MouseInput((int)keyType);
            }
        }

        private void Update()
        {
            foreach (var key in _keyInputs.Values)
            {
                key.IsButtonPressed = Input.GetKey(key.Code);
                key.IsButtonDown = Input.GetKeyDown(key.Code);
                key.IsButtonUp = Input.GetKeyUp(key.Code);
            }

            foreach (var key in _mouseInputs.Values)
            {
                key.IsButtonPressed = Input.GetMouseButton(key.MouseType);
                key.IsButtonDown = Input.GetMouseButtonDown(key.MouseType);
                key.IsButtonUp = Input.GetMouseButtonUp(key.MouseType);
            }

            MousePosition = Input.mousePosition;
        }

        public KeyInput GetButtonKey(KeyName name)
        {
            if (!_keyInputs.ContainsKey(name))
            {
                KeyButton keyButton = _inputSetting.KeyButtons.Find(x => EnumMapper.GetEnumType<KeyName>(x.name) == name);
                if (keyButton == null)
                {
                    Debug.LogError("Invalid Key Name");
                    return null;
                }
                _keyInputs[name] = new KeyInput(keyButton.button);
            }
            return _keyInputs[name];
        }

        public MouseInput GetMouseKey(MouseName name)
        {
            if (!_mouseInputs.ContainsKey(name))
            {
                _mouseInputs[name] = new MouseInput((int)name);
            }
            return _mouseInputs[name];
        }
    }
}