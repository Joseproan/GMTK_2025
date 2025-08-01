using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WindCorpse : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private int direction = 1; // 1 = right, -1 = left

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        
        if (layer == LayerMask.NameToLayer("Corpse") ||
            layer == LayerMask.NameToLayer("Ground") ||
            layer == LayerMask.NameToLayer("Cannon"))
        {
            // Flip direction on any collision
            direction *= -1;

            // Optional: flip sprite
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }
    }
}