using UnityEngine;

public enum EKeyBoardInput
{
    None = 0,

    W = 1,
    A = 2,
    S = 3,
    D = 4,

    SpaceBar = 5,

    Max,
}

public enum EMouseInput
{
    None = 0,

    LeftClick = 1,

    Max,
}

public class InputManager : MonoBehaviour
{
    static class Constants
    {
        public const int MouseLeftClickIndex = 0;
        public const int MouseRightClickIndex = 1;
        public const int MouseCenterClickIndex = 2;
    }

    private BaseController baseController;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        GetUserInput();
    }

    private void Initialize()
    {
        this.baseController = this.gameObject.GetComponent<BaseController>();
    }

    private void GetUserInput()
    {
#if (UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE)
        GetUserMouseInput();
        GetUserKeyBoardInput();
#endif
    }

    private void GetUserMouseInput()
    {
        if (Input.GetMouseButtonDown(Constants.MouseLeftClickIndex) == true)
        {
            this.baseController.MouseInputFunction(EMouseInput.LeftClick);
        }
    }

    // TO DO : 앞뒤, 왼오 중복 입력시 예외 처리 필요
    private void GetUserKeyBoardInput()
    {
        if (Input.GetKey(KeyCode.W) == true)
        {
            GetUserKeyBoardInputForward();
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            GetUserKeyBoardInputBack();
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            GetUserKeyBoardInputLeft();
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            GetUserKeyBoardInputRight();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            GetUserKeyBoardInputSpaceBar();
        }
    }

    private void GetUserKeyBoardInputForward()
    {
        this.baseController.KeyBoardInputFunction(EKeyBoardInput.W);
    }

    private void GetUserKeyBoardInputBack()
    {
        this.baseController.KeyBoardInputFunction(EKeyBoardInput.S);
    }

    private void GetUserKeyBoardInputLeft()
    {
        this.baseController.KeyBoardInputFunction(EKeyBoardInput.A);
    }

    private void GetUserKeyBoardInputRight()
    {
        this.baseController.KeyBoardInputFunction(EKeyBoardInput.D);
    }

    private void GetUserKeyBoardInputSpaceBar()
    {
        this.baseController.KeyBoardInputFunction(EKeyBoardInput.SpaceBar);
    }
}
