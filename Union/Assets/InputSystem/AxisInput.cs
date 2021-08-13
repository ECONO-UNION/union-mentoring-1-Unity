using InputSystem;

public class AxisInput
{
    private KeyInput _negativeKey;
    private KeyInput _postiveKey;

    public float Value
    {
        get
        {
            if (_negativeKey.IsKeyPressed && !_postiveKey.IsKeyPressed)
                return -1f;
            else if (!_negativeKey.IsKeyPressed && _postiveKey.IsKeyPressed)
                return 1f;
            return 0;
        }
    }

    public AxisInput(KeyInput negativeKey, KeyInput postiveKey)
    {
        _negativeKey = negativeKey;
        _postiveKey = postiveKey;
    }
}