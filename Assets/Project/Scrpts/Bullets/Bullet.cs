using System;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 5f;
    
    [Header("Corpse Settings")]
    public GameObject corpsePrefab;

    [Header("Scale Settings")]
    public float duration = 0.5f;
    public Ease easeType = Ease.OutBack; // gives the bounce effect
    public float delay = 0f;
    
    [Header("Particle Settings")]
    public ParticleSystem particlePrefab;
    public Color particleColor = Color.white;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float lifeTimer;
    [HideInInspector] public GameObject parentCannon;

    public string bulletElement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayBounce();
    }
    
    private void PlayBounce()
    {
        transform.localScale = Vector3.zero; // start at 0
        transform.DOScale(Vector3.one, duration)
            .SetEase(easeType)
            .SetDelay(delay);
    }

    private void OnDisable()
    {
        SpawnParticle();
    }

    private void SpawnParticle()
    {
        if (particlePrefab == null) return;

        // Instantiate the particle system
        ParticleSystem ps = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // Set the color
        var main = ps.main;
        main.startColor = particleColor;

        // Optional: Destroy after duration
        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
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
                Instantiate(corpsePrefab, collision.gameObject.transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX("Death", bulletElement);

                ReturnToPool();
            }
            
        }
        // If hit corpse, ground, or cannon
        else if (layer == LayerMask.NameToLayer("Corpse") ||
                 layer == LayerMask.NameToLayer("Ground") ||
                 layer == LayerMask.NameToLayer("Cannon") && collision.gameObject != parentCannon)
        {
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}