namespace InputSystem
{
    public class InputButtonEvent
    {
        public bool IsButtonPressed { get; set; }
        public bool IsButtonUp { get; set; }
        public bool IsButtonDown { get; set; }
        public string Name { get; private set; }

        public InputButtonEvent(string name)
        {
            Name = name;
        }
    }

    public class InputAxisEvent
    {
        public float Axis { get; set; }
        public string Name { get; private set; }

        public InputAxisEvent(string name)
        {
            Name = name;
        }
    }
}