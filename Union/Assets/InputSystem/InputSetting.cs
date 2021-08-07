using UnityEngine;
using System;
using System.Collections.Generic;

namespace InputSystem
{
    [Serializable]
    public class KeyButton
    {
        public string name;
        public KeyCode button;
    }

    [Serializable]
    public class MouseButton
    {
        public enum Type
        {
            Left,
            Right,
            Middle,
        }
        public Type type;
    }

    public class InputSetting : ScriptableObject
    {
        public List<KeyButton> keyButtons;
        public List<MouseButton> mouseButtons;
    }
}
