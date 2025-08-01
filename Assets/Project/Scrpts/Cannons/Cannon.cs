using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    [Header("Shooting")]
    public Transform firePoint;
    public float shootCooldown = 1.5f;
    public string bulletTag = "EnemyBullet";

    public enum Direction { Up, Down, Left, Right }
    public Direction shootDirection = Direction.Right;

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
                Vector2 direction = GetShootDirection();
                Shoot(direction);
                shootTimer = shootCooldown;
            }
        }
    }

    private Vector2 GetShootDirection()
    {
        return shootDirection switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => Vector2.right
        };
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