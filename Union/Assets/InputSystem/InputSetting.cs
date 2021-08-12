using UnityEngine;
using System;
using System.Collections.Generic;

namespace InputSystem
{
    [Serializable]
    public class KeyButton
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private KeyCode _button;

        public string Name { get => _name;}
        public KeyCode Button { get => _button;}
    }

    [Serializable]
    public class MouseButton
    {
        public enum buttonType
        {
            Left,
            Right,
            Middle,
        }

        [SerializeField]
        private buttonType _type;
        public buttonType Type { get => _type; }
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
