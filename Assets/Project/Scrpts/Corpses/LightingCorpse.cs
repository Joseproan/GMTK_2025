using UnityEngine;

public class LightingCorpse : MonoBehaviour
{
    public float pushForce = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Usamos el punto de contacto real para mayor precisión
                ContactPoint2D contact = collision.contacts[0];
                Vector2 pushDir = (contact.point - (Vector2)transform.position).normalized;

                playerRb.linearVelocity = Vector2.zero; // Reset para evitar interferencias
                playerRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
