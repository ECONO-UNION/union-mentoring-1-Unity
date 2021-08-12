using UnityEngine;

namespace InputSystem
{
    public class KeyInput
    {
        public bool IsButtonPressed { get; set; }
        public bool IsButtonUp { get; set; }
        public bool IsButtonDown { get; set; }
        public KeyCode Code { get; set; }

        public KeyInput(KeyCode code)
        {
            Code = code;
        }
    }

    public class MouseInput
    {
        public bool IsButtonPressed { get; set; }
        public bool IsButtonUp { get; set; }
        public bool IsButtonDown { get; set; }
        public int MouseType { get; private set; }

        public MouseInput(int mouseType)
        {
            MouseType = mouseType;
        }
    }
}