using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 5f;

    [Header("Shooting")]
    public Transform firePoint;
    public float shootCooldown = 1.5f;
    public string bulletTag = "EnemyBullet";

    private Transform player;
    private float shootTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot(transform.up);
                AudioManager.Instance.PlaySFX("Cannon", bulletTag);
                shootTimer = shootCooldown;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bulletObj = ObjectPooler.Instance.SpawnFromPool(bulletTag, firePoint.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.parentCannon = gameObject;
        bullet.SetDirection(direction.normalized);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}