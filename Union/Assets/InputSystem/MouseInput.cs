using UnityEngine;

namespace InputSystem
{
    public class MouseInput
    {
        public Vector2 Position { get; private set; }
        public float WeightX { get; private set; }
        public float WeightY { get; private set; }

        public void Run()
        {
            Position = Input.mousePosition;
            WeightX = Input.GetAxis("Mouse X");
            WeightY = Input.GetAxis("Mouse Y");
        }
    }
}
