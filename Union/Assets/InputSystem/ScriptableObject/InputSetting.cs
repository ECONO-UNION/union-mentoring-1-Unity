using UnityEngine;
using System;

namespace InputSystem
{
    [Serializable]
    public class InputAxis
    {
        public string name;
        public string negativeButton;
        public string positiveButton;
    }

    [Serializable]
    public class InputButton
    {
        public string name;
        public string button;
    }

    [CreateAssetMenu(fileName = "InputSetting", menuName = "InputSystem/InputSetting")]
    public class InputSetting : ScriptableObject
    {
        public InputAxis[] inputAxes;
        public InputButton[] inputButtons;
    }
}
