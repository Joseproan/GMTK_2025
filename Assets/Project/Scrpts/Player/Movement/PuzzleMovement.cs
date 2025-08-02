using UnityEngine;

public class PuzzleMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 15f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Collider2D feetCollider;
    [SerializeField] private float groundRayLength = 0.1f;
    private bool _isGrounded;

    float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ¡Faltaba esto!
   
    }

    void Update()
    {


        Flip();
    }

    private void FixedUpdate()
    {

        horizontal = InputManager.Movement.x;
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (InputManager.JumpWasPressed && _isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void CheckIsGrounded()
    {
        Vector2 origin = new Vector2(feetCollider.bounds.center.x, feetCollider.bounds.min.y);
        Vector2 size = new Vector2(feetCollider.bounds.size.x, groundRayLength);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector2.down, 0f, groundLayer);
        _isGrounded = hit.collider != null;
    }
}
