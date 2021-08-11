using UnityEngine;

public class BaseController : MonoBehaviour
{
    static class Constants
    {
        public const int MaxJumpCount = 2;
    }

    private Movement movement;
    private Rigidbody controllerRigidbody;

    private bool onGround;

    private int currentJumpCount;

    private float moveSpeed;
    private float jumpingPower;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        this.movement = this.gameObject.GetComponent<Movement>();
        this.controllerRigidbody = this.gameObject.GetComponent<Rigidbody>();

        this.currentJumpCount = 0;

        this.moveSpeed = 10.0f;
        this.jumpingPower = 500.0f;

        this.controllerRigidbody.mass = 1.5f;
    }

    public void KeyBoardInputFunction(EKeyBoardInput eKeyBoardInput)
    {
        switch (eKeyBoardInput)
        {
            case EKeyBoardInput.W:
            case EKeyBoardInput.A:
            case EKeyBoardInput.S:
            case EKeyBoardInput.D:
                KeyBoardInputFunctionMove(eKeyBoardInput);
                break;
            case EKeyBoardInput.SpaceBar:
                KeyBoardInputFunctionSpaceBar();
                break;
            default:
                break;
        }
    }

    private void KeyBoardInputFunctionMove(EKeyBoardInput eKeyBoardInput)
    {
        if (CanMove() == false)
        {
            return;
        }

        switch (eKeyBoardInput)
        {
            case EKeyBoardInput.W:
                KeyBoardInputFunctionW();
                break;
            case EKeyBoardInput.A:
                KeyBoardInputFunctionA();
                break;
            case EKeyBoardInput.S:
                KeyBoardInputFunctionS();
                break;
            case EKeyBoardInput.D:
                KeyBoardInputFunctionD();
                break;
            default:
                break;
        }
    }

    private void KeyBoardInputFunctionW()
    {
        Vector3 force = Vector3.forward;
        force.z *= this.moveSpeed;

        this.movement.AddForce(force);
        //this.movement.Translate(Vector3.forward, this.moveSpeed);
    }

    private void KeyBoardInputFunctionA()
    {
        Vector3 force = Vector3.left;
        force.x *= this.moveSpeed;

        this.movement.AddForce(force);
        //this.movement.Translate(Vector3.left, this.moveSpeed);
    }

    private void KeyBoardInputFunctionS()
    {
        Vector3 force = Vector3.back;
        force.z *= this.moveSpeed;

        this.movement.AddForce(force);
        //this.movement.Translate(Vector3.back, this.moveSpeed);
    }

    private void KeyBoardInputFunctionD()
    {
        Vector3 force = Vector3.right;
        force.x *= this.moveSpeed;

        this.movement.AddForce(force);
        //this.movement.Translate(Vector3.right, this.moveSpeed);
    }

    private void KeyBoardInputFunctionSpaceBar()
    {
        if (CanJump() == false)
        {
            return;
        }

        Vector3 force = Vector3.up;
        force.y *= this.jumpingPower;

        this.movement.AddForce(force);
    }

    public void MouseInputFunction(EMouseInput eMouseInput)
    {
        switch (eMouseInput)
        {
            case EMouseInput.LeftClick:
                MouseInputFunctionLeftClickCase();
                break;
        }
    }

    private void MouseInputFunctionLeftClickCase()
    {

    }

    private bool CanMove()
    {
        if (this.onGround == false)
        {
            return false;
        }

        if (Union.Services.Game.GameLogic.Instance.IsPlaying() == false)
        {
            return false;
        }

        return true;
    }

    private bool CanJump()
    {
        if (this.onGround == false)
        {
            return false;
        }

        if (this.currentJumpCount >= Constants.MaxJumpCount)
        {
            return false;
        }

        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}
