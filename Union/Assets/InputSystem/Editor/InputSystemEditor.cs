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

        [SerializeField]
        private string _directory = "InputSystem";
        [SerializeField]
        private string _settingName = "InputSetting";

        private GUIStyle _titleStyle = new GUIStyle();
        private GUIStyle _subLabelStyle = new GUIStyle();
        private int _textFieldSize = 250;
        private int _buttonsSize = 150;
        private int _spaceSize = 20;

        [MenuItem("InputSystem/Set Input system")]
        private static void OpenEditor()
        {
            GetWindow<InputSystemEditor>("InputSystem");
        }

        private void OnEnable()
        {
            _titleStyle.alignment = TextAnchor.MiddleCenter;
            _titleStyle.fontStyle = FontStyle.Bold;
            _titleStyle.normal.textColor = new Color(0f, 1f, 0.6f);
            _titleStyle.fontSize = 24;

            _subLabelStyle.alignment = TextAnchor.MiddleRight;
            _subLabelStyle.normal.textColor = Color.gray;
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
            GUILayout.Space(_spaceSize);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Directory Path : ");
            GUILayout.Label("Assets/Resources/", _subLabelStyle);
            _directory = EditorGUILayout.TextField(_directory, GUILayout.Width(_textFieldSize));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("File Name : ");
            _settingName = EditorGUILayout.TextField(_settingName, GUILayout.Width(_textFieldSize));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Refresh", GUILayout.Width(_buttonsSize)))
            {
                OnRefreshButton();
            }
            GUILayout.Space(_spaceSize);
        }

        private void OnRefreshButton()
        {
            if (string.IsNullOrEmpty(_directory) || string.IsNullOrEmpty(_settingName))
            {
                EditorUtility.DisplayDialog("", "잘못된 경로입니다.\n폴더경로 또는 파일이름을 다시 입력해주세요. ", "확인");
                return;
            }

            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            string path = $"Assets/Resources/{_directory}/{_settingName}.asset";
            _inputSetting = AssetDatabase.LoadAssetAtPath(path, typeof(InputSetting)) as InputSetting;
            if (_inputSetting == null)
            {
                _inputSetting = CreateInstance<InputSetting>();
                AssetDatabase.CreateAsset(_inputSetting, path);
                AssetDatabase.ImportAsset(path);
            }
            _serializedObject = new SerializedObject(_inputSetting);
        }

        private void ShowMiddleMenu()
        {
            if (_inputSetting == null || _serializedObject == null)
                return;

            SerializedProperty keyButtonProperty = _serializedObject.FindProperty("_keyButtons");
            SerializedProperty mouseButtonProperty = _serializedObject.FindProperty("_mouseButtons");
            EditorGUILayout.PropertyField(keyButtonProperty, true);
            EditorGUILayout.PropertyField(mouseButtonProperty, true);
            _serializedObject.ApplyModifiedProperties();
        }

        private void ShowBottomMenu()
        {
            if (_inputSetting == null || _serializedObject == null)
                return;

            GUI.color = Color.cyan;
            if (GUILayout.Button("Apply Input System", GUILayout.Width(_buttonsSize)))
            {
                CreateEnumClass(_inputSetting.KeyButtons, _inputSetting.MouseButtons);
                AssetDatabase.SaveAssets();
            }
        }

        public void CreateEnumClass(List<KeyButton> inputButtons, List<MouseButton> mouseButtons)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// Input System에 의해서 자동으로 생성되는 Enum입니다.");
            sb.AppendLine("namespace InputSystem");
            sb.AppendLine("{");

            sb.AppendLine("    static class InputSystemSetting");
            sb.AppendLine("    {");
            sb.AppendLine($"        public const string Path = \"{_directory}/{_settingName}\";");
            sb.AppendLine("    }");
            sb.AppendLine();

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