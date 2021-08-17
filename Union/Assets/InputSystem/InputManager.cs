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

        private InputSetting _inputSetting;

        public Dictionary<KeyName, KeyInput> KeyInputs { get; private set; }
        public Vector2 MousePosition { get; private set; }

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
            KeyInputs = new Dictionary<KeyName, KeyInput>();
            foreach (var keyButton in _inputSetting.Key)
            {
                KeyName name = EnumMapper.GetEnumType<KeyName>(keyButton.Name);
                KeyCode code = GetUserSettingKeyCode(name);
                if (code == KeyCode.None)
                    code = keyButton.Code;

                KeyInputs[name] = new KeyInput(code);
            }
        }

        private KeyCode GetUserSettingKeyCode(KeyName keyName)
        {
            string userSettingKey = PlayerPrefs.GetString(keyName.ToString());
            if (string.IsNullOrEmpty(userSettingKey))
                return KeyCode.None;
            else
                return EnumMapper.GetEnumType<KeyCode>(userSettingKey);
        }

        private void Update()
        {
            foreach (var key in KeyInputs.Values)
            {
                key.IsKeyPressed = Input.GetKey(key.Code);
                key.IsKeyDown = Input.GetKeyDown(key.Code);
                key.IsKeyUp = Input.GetKeyUp(key.Code);
            }
            MousePosition = Input.mousePosition;
        }

        public KeyInput GetKey(KeyName name)
        {
            if (!KeyInputs.ContainsKey(name))
            {
                Key keyButton = _inputSetting.Key.Find(x => EnumMapper.GetEnumType<KeyName>(x.Name) == name);
                if (keyButton == null)
                {
                    Debug.LogError("Invalid Key Name");
                    return null;
                }
                KeyInputs[name] = new KeyInput(keyButton.Code);
            }
            return KeyInputs[name];
        }

        public void ChangeKey(KeyName targetKey, KeyCode changeKeyCode)
        {
            KeyInputs[targetKey].Code = changeKeyCode;
            PlayerPrefs.SetString(targetKey.ToString(), changeKeyCode.ToString());
        }

        // KEY TEST 용도입니다 //
        private void OnGUI()
        {
            int height = 0;
            foreach (var key in KeyInputs)
            {
                GUI.Label(new Rect(100, 40 + height, 80, 20), key.Key.ToString());
                GUI.Label(new Rect(20, 40 + height, 80, 20), key.Value.Code.ToString());
                height += 20;
            }
        }
    }
}