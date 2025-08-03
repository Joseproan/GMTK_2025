using UnityEngine;

public class WindCorpse : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D rb;
    private int direction = 1; // 1 = right, -1 = left

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed * Time.deltaTime, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        Debug.Log(layer);
        if (layer == LayerMask.NameToLayer("Corpse") ||
            layer == LayerMask.NameToLayer("Ground") ||
            layer == LayerMask.NameToLayer("Cannon"))
        {
            Debug.Log("invoked");
            // Flip direction on any collision
            direction *= -1;

            // Optional: flip sprite
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }
    }
}