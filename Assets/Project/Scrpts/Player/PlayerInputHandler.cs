using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //CONTROLS SCRIPT
    public Controls playerControls;

    //INPUTS
    private InputAction move;
    private InputAction jump;
    private InputAction pause;

    //BOOLS
    private Vector2 moveInput;
    private bool jumping, setPause;
    private void Awake()
    {
        playerControls = new Controls();
    }

    private void Update()
    {
        MoveInput();
    }
    private void OnEnable()
    {
        // Movimiento: habilitar la acci�n de movimiento.
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += JumpInput;

        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += PauseMenu;

    }

    private void OnDisable()
    {
        // Movimiento: deshabilitar la acci�n de movimiento.
        move.Disable();

        jump.Disable();

        pause.Disable();
    }
    //MOVE INPUT
    public void MoveInput()
    {
        moveInput = move.ReadValue<Vector2>();
    }

    //SETTERS
    private void JumpInput(InputAction.CallbackContext context)
    {
        jumping = true; // Se activa solo una vez al presionar el botón
    }

    private void PauseMenu(InputAction.CallbackContext context)
    {
        setPause = true;
    }
    //GETTERS
    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
    public bool GetJumpInput(bool input)
    {
        input = jumping;
        jumping = false;
        return input;
    }

    public bool GetPause(bool input)
    {
        input = setPause;
        setPause = false;
        return input;
    }
}
