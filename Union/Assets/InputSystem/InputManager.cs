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
        private InputSetting _inputSetting;

        public MouseInput MouseInput { get; private set; }

        private void Awake()
        {
            _inputSetting = Resources.Load<InputSetting>(InputSystemSetting.Path);
            if (_inputSetting == null)
            {
                Debug.LogError("Input System을 설정하지 않았습니다");
                return;
            }
            BindKey();
        }

        private void BindKey()
        {
            foreach (var keyButton in _inputSetting.Key)
            {
                KeyName name = EnumMapper.GetEnumType<KeyName>(keyButton.Name);
                _keyInputs[name] = new KeyInput(keyButton.Code);
            }
        }

        private void Update()
        {
            foreach (var key in _keyInputs.Values)
            {
                key.IsKeyPressed = Input.GetKey(key.Code);
                key.IsKeyDown = Input.GetKeyDown(key.Code);
                key.IsKeyUp = Input.GetKeyUp(key.Code);
            }
            MouseInput.Run();
        }

        public KeyInput GetKey(KeyName name)
        {
            if (!_keyInputs.ContainsKey(name))
            {
                Key keyButton = _inputSetting.Key.Find(x => EnumMapper.GetEnumType<KeyName>(x.Name) == name);
                if (keyButton == null)
                {
                    Debug.LogError("Invalid Key Name");
                    return null;
                }
                _keyInputs[name] = new KeyInput(keyButton.Code);
            }
            return _keyInputs[name];
        }

        // KEY TEST 용도입니다 //
        private void OnGUI()
        {
            int height = 0;
            foreach (var key in _keyInputs)
            {
                GUI.Label(new Rect(100, 40 + height, 80, 20), key.Key.ToString());
                GUI.Label(new Rect(20, 40 + height, 80, 20), key.Value.Code.ToString());
                height += 20;
            }
        }
    }
}