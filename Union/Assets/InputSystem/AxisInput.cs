namespace InputSystem
{
    public class AxisInput
    {
        private KeyInput _negativeKey;
        private KeyInput _positiveKey;

        public float Value
        {
            get
            {
                if (_negativeKey.IsKeyPressed && !_positiveKey.IsKeyPressed)
                    return -1f;
                else if (!_negativeKey.IsKeyPressed && _positiveKey.IsKeyPressed)
                    return 1f;
                return 0;
            }
        }

        public AxisInput(KeyInput negativeKey, KeyInput postiveKey)
        {
            _negativeKey = negativeKey;
            _positiveKey = postiveKey;
        }
    }
}