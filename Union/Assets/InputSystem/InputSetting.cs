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
        [SerializeField]
        private List<KeyButton> _keyButtons;
        [SerializeField]
        private List<MouseButton> _mouseButtons;

        public List<KeyButton> KeyButtons { get => _keyButtons; }
        public List<MouseButton> MouseButtons { get => _mouseButtons; }
    }
}
