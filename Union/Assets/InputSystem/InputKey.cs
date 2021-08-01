namespace InputSystem
{
    public class ButtonKey
    {
        public bool IsButtonPressed { get; set; }
        public bool IsButtonUp { get; set; }
        public bool IsButtonDown { get; set; }
        public string Name { get; private set; }

        public ButtonKey(string name)
        {
            Name = name;
        }
    }

    public class AxisKey
    {
        public float Value { get; set; }
        public string Name { get; private set; }

        public AxisKey(string name)
        {
            Name = name;
        }
    }
}