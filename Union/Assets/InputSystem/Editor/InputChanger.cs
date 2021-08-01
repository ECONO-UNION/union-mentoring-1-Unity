using UnityEditor;

namespace InputSystem.Editor
{
    public class InputChanger
    {
        private SerializedObject _unityInputAssest;
        private SerializedProperty _unityInputProperty;

        public void ChangeInnerInput(InputSetting currentObject)
        {
            LoadInnerInput();
            ClearInnerInput();
            foreach (InputAxis inputAxis in currentObject.inputAxes)
            {
                AddInputAxis(inputAxis);
            }
            foreach (InputButton inputButton in currentObject.inputButtons)
            {
                AddInputButton(inputButton);
            }
        }

        private void LoadInnerInput()
        {
            _unityInputAssest = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            _unityInputProperty = _unityInputAssest.FindProperty("m_Axes");
        }

        private void ClearInnerInput()
        {
            _unityInputProperty.ClearArray();
            _unityInputAssest.ApplyModifiedProperties();
        }

        private void AddInputAxis(InputAxis input)
        {
            _unityInputProperty.arraySize++;
            SerializedProperty currentInputProperty = _unityInputProperty.GetArrayElementAtIndex(_unityInputProperty.arraySize - 1);
            GetChildProperty(currentInputProperty, "m_Name").stringValue = input.name;
            GetChildProperty(currentInputProperty, "negativeButton").stringValue = input.negativeButton;
            GetChildProperty(currentInputProperty, "positiveButton").stringValue = input.positiveButton;
            _unityInputAssest.ApplyModifiedProperties();
        }

        private void AddInputButton(InputButton input)
        {
            _unityInputProperty.arraySize++;
            SerializedProperty currentInputProperty = _unityInputProperty.GetArrayElementAtIndex(_unityInputProperty.arraySize - 1);
            GetChildProperty(currentInputProperty, "m_Name").stringValue = input.name;
            GetChildProperty(currentInputProperty, "negativeButton").stringValue = "";
            GetChildProperty(currentInputProperty, "positiveButton").stringValue = input.button.ToLower();
            _unityInputAssest.ApplyModifiedProperties();
        }

        private SerializedProperty GetChildProperty(SerializedProperty input, string keyName)
        {
            SerializedProperty inputClone = input.Copy();

            inputClone.Next(true);
            while (inputClone.name != keyName)
            {
                inputClone.Next(false);
                // TODO : 못찾을경우 예외처리해줘야함
            }
            return inputClone;
        }
    }
}
