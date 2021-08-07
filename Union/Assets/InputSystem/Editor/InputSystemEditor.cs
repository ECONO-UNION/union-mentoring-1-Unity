using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

namespace InputSystem
{
    public class InputSystemEditor : EditorWindow
    {
        private InputSetting _inputSetting;
        private SerializedObject _serializedObject;
        private GUIStyle _titleStyle = new GUIStyle();

        private string _directory = "Assets/Resources/InputSystem";
        private string _settingName = "InputSetting.asset";

        [MenuItem("InputSystem/Set Input system")]
        static void open()
        {
            GetWindow<InputSystemEditor>("InputSystem");
        }

        private void OnEnable()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            string path = $"{_directory}/{_settingName}";
            _inputSetting = AssetDatabase.LoadAssetAtPath(path, typeof(InputSetting)) as InputSetting;
            if (_inputSetting == null)
            {
                _inputSetting = CreateInstance<InputSetting>();
                AssetDatabase.CreateAsset(_inputSetting, path);
                AssetDatabase.ImportAsset(path);
            }

            _serializedObject = new SerializedObject(_inputSetting);
            _titleStyle.alignment = TextAnchor.MiddleCenter;
            _titleStyle.fontStyle = FontStyle.Bold;
            _titleStyle.normal.textColor = new Color(0f, 1f, 0.6f);
            _titleStyle.fontSize = 24;
        }

        private void OnGUI()
        {
            ShowTopMenu();
            ShowMiddleMenu();
            ShowBottomMenu();
        }

        private void ShowTopMenu()
        {
            GUILayout.Label("Input System", _titleStyle);
        }

        private void ShowMiddleMenu()
        {
            SerializedProperty keyButtonProperty = _serializedObject.FindProperty("keyButtons");
            SerializedProperty mouseButtonProperty = _serializedObject.FindProperty("mouseButtons");
            EditorGUILayout.PropertyField(keyButtonProperty, true);
            EditorGUILayout.PropertyField(mouseButtonProperty, true);
            _serializedObject.ApplyModifiedProperties();
        }

        private void ShowBottomMenu()
        {
            GUI.color = Color.cyan;
            if (GUILayout.Button("Apply Input System"))
            {
                CreateEnumClass(_inputSetting.keyButtons, _inputSetting.mouseButtons);
            }
        }

        public void CreateEnumClass(List<KeyButton> inputButtons, List<MouseButton> mouseButtons)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// Input System에 의해서 자동으로 생성되는 Enum입니다.");
            sb.AppendLine("namespace InputSystem");
            sb.AppendLine("{");

            if (inputButtons.Count > 0)
            {
                sb.AppendLine("    public enum KeyName");
                sb.AppendLine("    {");
                foreach (KeyButton inputButton in inputButtons)
                {
                    sb.AppendLine($"        {inputButton.name},");
                }
                sb.AppendLine("    }");
                sb.AppendLine();
            }

            if (mouseButtons.Count > 0)
            {
                sb.AppendLine("    public enum MouseName");
                sb.AppendLine("    {");
                var mouseButtonTypes = Enum.GetValues(typeof(MouseButton.Type));
                HashSet<int> hasName = new HashSet<int>();
                foreach (MouseButton mouseButton in mouseButtons)
                {
                    hasName.Add((int)mouseButton.type);
                }
                foreach (var mouseButtonType in mouseButtonTypes)
                {
                    if (hasName.Contains((int)mouseButtonType))
                    {
                        sb.AppendLine($"        {mouseButtonType} = {(int)mouseButtonType},");
                    }
                }
                sb.AppendLine("    }");
            }
            sb.AppendLine("}");
            string path = Application.dataPath + "/InputSystem/InputNames.cs";
            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();
        }
    }
}