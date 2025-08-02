using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;

    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHold;
    public static bool JumpWasReleased;
    public static bool RunIsHold;
    public static bool InteractWasPressed;
    public static bool ContinueStoryWasPressed;
    public static bool UpArrowWasPressed;
    public static bool DownArrowWasPressed;
    public static bool SucideWasPressed;
    public static bool PauseWasPressed;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _interactAction;
    private InputAction _continueStoryAction;
    private InputAction _upArrowAction;
    private InputAction _downArrowAction;
    private InputAction _suicideAction;
    private InputAction _pauseAction;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        _interactAction = PlayerInput.actions["Interact"];
        _continueStoryAction = PlayerInput.actions["ContinueStory"];
        _upArrowAction = PlayerInput.actions["UpArrow"];
        _downArrowAction = PlayerInput.actions["DownArrow"];
        _suicideAction = PlayerInput.actions["Suicide"];
        _pauseAction = PlayerInput.actions["Pause"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHold = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();

        RunIsHold = _runAction.IsPressed();

        InteractWasPressed = _interactAction.WasPressedThisFrame();
        
        ContinueStoryWasPressed = _continueStoryAction.WasPressedThisFrame();
        
        UpArrowWasPressed = _upArrowAction.WasPressedThisFrame();
        DownArrowWasPressed = _downArrowAction.WasPressedThisFrame();

        SucideWasPressed = _suicideAction.WasPressedThisFrame();

        PauseWasPressed = _pauseAction.WasPressedThisFrame();
    }
}
