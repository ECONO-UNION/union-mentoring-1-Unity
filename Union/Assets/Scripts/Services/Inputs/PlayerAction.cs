using UnityEngine;

namespace Union.Services.Inputs
{
    public sealed class PlayerAction : IActionProvider
    {
        public bool IsPressed { get; private set; }
        public bool WasPressed { get; private set; }
        public bool WasReleased { get; private set; }

        private string key;

        public PlayerAction(string key)
        {
            this.key = key; 
        }

        public void Update()
        {
            bool stay = Input.GetButton(key);
            bool enter = Input.GetButtonDown(key);
            bool release = Input.GetButtonUp(key);

            if (enter && !IsPressed)
                IsPressed = true;
            else if (IsPressed && stay)
            {
                WasPressed = true;
                IsPressed = false;
            }

            if (release)
            {
                WasPressed = false;
                WasReleased = true;
            }
        }
    }
}
