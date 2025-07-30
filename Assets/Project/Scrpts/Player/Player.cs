using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IMovable,IInputs
{

    #region Components
    public Rigidbody2D rb { get; set; }
    public PlayerInputHandler inputHandler { get; set; }
    #endregion

    #region State Machine Variables
    public PlayerStateMachine stateMachine { get; set; }
    public PlayerIdleState idleState { get; set; }
    public PlayerMoveState moveState { get; set; }
    public PlayerJumpState jumpState { get; set; }

    #endregion

    #region Input Checks
    public bool jumping { get; set; }
    public Vector2 moveInput { get; set; }
    #endregion

    //INSPECTOR VARIABLES ---------------------------->

    [Header("Stats")]
    public float speed;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        inputHandler = GetComponent<PlayerInputHandler>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);
        jumpState = new PlayerJumpState(this, stateMachine);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        GetInputs();
        stateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentPlayerState.PhysicsUpdate();
    }
    public void GetInputs()
    {
        moveInput = inputHandler.GetMoveInput();
        jumping = inputHandler.GetJumpInput(jumping);
        //spinning = inputHandler.GetSpinOrbInput(spinning);
    }
    public void PlayerMovement()
    {
        rb.AddForce(moveInput * speed);
    }

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    { 
    
    }
    public enum AnimationTriggerType
    {

    }
    #endregion
}
