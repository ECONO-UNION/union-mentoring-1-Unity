using UnityEngine;
using UnityEditor;

namespace InputSystem.Editor
{
    [CustomEditor(typeof(InputSetting))]
    public class InputSettingEditor : UnityEditor.Editor
    {
        private InputChanger _inputChanger = new InputChanger();
        private InputEnumCreator _inputEnumCreator = new InputEnumCreator();

        private Color _guiColorTheme = new Color(0, 1, 0.6f);
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.color = _guiColorTheme;

            InputSetting currentObject = (InputSetting)target;
            if (GUILayout.Button("Connect Input Setting"))
            {
                _inputChanger.ChangeInnerInput(currentObject);
                _inputEnumCreator.CreateEnumClass(currentObject.inputAxes, currentObject.inputButtons);
            }
        }
    }
}