using InputSystem;
using UnityEngine;

public class PlayerInput
{
    public AxisInput Horizontal { get; private set; }
    public AxisInput Vertical { get; private set; }
    public KeyInput Walk { get; private set; }
    public KeyInput Sprint { get; private set; }
    public KeyInput Crouch { get; private set; }
    public KeyInput Jump { get; private set; }
    public MouseInput Attack { get; private set; }
    public Vector2 MousePosition { get; private set; }

    public PlayerInput()
    {
        Horizontal = new AxisInput(InputSystem.InputManager.Instance.GetButtonKey(KeyName.Left), InputSystem.InputManager.Instance.GetButtonKey(KeyName.Right));
        Vertical = new AxisInput(InputSystem.InputManager.Instance.GetButtonKey(KeyName.Down), InputSystem.InputManager.Instance.GetButtonKey(KeyName.Up));
        Walk = InputSystem.InputManager.Instance.GetButtonKey(KeyName.Walk);
        Sprint = InputSystem.InputManager.Instance.GetButtonKey(KeyName.Sprint);
        Crouch = InputSystem.InputManager.Instance.GetButtonKey(KeyName.Crouch);
        Jump = InputSystem.InputManager.Instance.GetButtonKey(KeyName.Jump);
        Attack = InputSystem.InputManager.Instance.GetMouseKey(MouseName.Left);
        MousePosition = InputSystem.InputManager.Instance.MousePosition;
    }
}