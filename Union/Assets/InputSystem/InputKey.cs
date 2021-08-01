using System;

namespace InputSystem
{
    public class InputButtonEvent
    {
        public Action onPressedKey;
        public Action onUpKey;
        public Action onDownKey;

        private string _name;
        public string Name { get => _name; }

        public InputButtonEvent(string name)
        {
            _name = name;
        }
    }

    public class InputAxisEvent
    {
        public Action<float> onPressedKey;

        private string _name;
        public string Name { get => _name; }

        public InputAxisEvent(string name)
        {
            _name = name;
        }
    }
}