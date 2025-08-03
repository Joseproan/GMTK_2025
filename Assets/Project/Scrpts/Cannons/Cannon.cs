using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 5f;

    [Header("Shooting")]
    public Transform firePoint;
    public float shootCooldown = 1.5f;
    public string bulletTag = "EnemyBullet";
    
    public enum Direction { Left, Right, Up, Down }

    public Direction direction = Direction.Up;
    
    private Transform player;
    private float shootTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null) return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot(GetDirection());
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

    Vector3 GetDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                return transform.up;
            case Direction.Down:
                return -transform.up;
            case Direction.Left:
                return -transform.right;
            case Direction.Right:
                return transform.right;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}