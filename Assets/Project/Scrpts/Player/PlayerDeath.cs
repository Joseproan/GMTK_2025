using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerDeath : MonoBehaviour
{
    public Transform checkpoint; // Assign this in the inspector or dynamically
    public bool isAlive = true;
    public CinemachineCamera virtualCamera;
    
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;

        // Optional: disable controls, collider, etc.
        SetPlayerActive(false);
        virtualCamera.Follow = null;
        StartCoroutine(RespawnAfterDelay(1f));
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Move to checkpoint
        transform.position = checkpoint.position;
        virtualCamera.Follow = transform;

        // Optional: reset velocity if using Rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Reactivate player
        SetPlayerActive(true);
        isAlive = true;
    }

    private void SetPlayerActive(bool active)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = active;
        if (playerCollider != null) playerCollider.enabled = active;
        if (playerMovement != null) playerMovement.enabled = active;
    }
}