using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 5f;
    
    [Header("Corpse Settings")]
    public GameObject corpsePrefab;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float lifeTimer;
    [HideInInspector] public GameObject parentCannon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        lifeTimer = lifeTime; // reset life
        gameObject.SetActive(true); // in case it was inactive from pool
    }

    private void Update()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        // If hit player
        if (layer == LayerMask.NameToLayer("Player"))
        {
            // kill player
            var playerDeath = collision.gameObject.GetComponentInParent<PlayerDeath>();
            if (playerDeath != null && playerDeath.isAlive)
            {
                playerDeath.Die();
                // instantiate corpse
                Instantiate(corpsePrefab, transform.position, Quaternion.identity);
                
                ReturnToPool();
            }
            
        }
        // If hit corpse, ground, or cannon
        else if (layer == LayerMask.NameToLayer("Corpse") ||
                 layer == LayerMask.NameToLayer("Ground") ||
                 layer == LayerMask.NameToLayer("Cannon") && collision.gameObject != parentCannon)
        {
            Debug.Log(collision.gameObject.name);
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}