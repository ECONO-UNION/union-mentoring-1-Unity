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
    public KeyInput Attack { get; private set; }
    public MouseInput MouseInput { get; private set; }

    public PlayerInput()
    {
        Horizontal = new AxisInput(InputSystem.InputManager.Instance.GetKey(KeyName.Left), InputSystem.InputManager.Instance.GetKey(KeyName.Right));
        Vertical = new AxisInput(InputSystem.InputManager.Instance.GetKey(KeyName.Down), InputSystem.InputManager.Instance.GetKey(KeyName.Up));
        Walk = InputSystem.InputManager.Instance.GetKey(KeyName.Walk);
        Sprint = InputSystem.InputManager.Instance.GetKey(KeyName.Sprint);
        Crouch = InputSystem.InputManager.Instance.GetKey(KeyName.Crouch);
        Jump = InputSystem.InputManager.Instance.GetKey(KeyName.Jump);
        Attack = InputSystem.InputManager.Instance.GetKey(KeyName.Attack);
        MouseInput = InputSystem.InputManager.Instance.MouseInput;
    }
}