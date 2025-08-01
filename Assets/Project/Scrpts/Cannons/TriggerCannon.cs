using UnityEngine;

public class TriggerCannon : MonoBehaviour
{
    [Header("Corpse Settings")]
    public GameObject corpsePrefab;
    
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
            }
            
        }
    }
}
