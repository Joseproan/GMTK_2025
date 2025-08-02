using UnityEngine;

public class PlayerSuicide : MonoBehaviour
{
    private PlayerDeath playerDeath;

    [Header("Corpse Prefab")]
    public GameObject corpsePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDeath = GetComponentInParent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.SucideWasPressed && playerDeath != null && playerDeath.isAlive && playerDeath != null && playerDeath.isAlive)
        {
            playerDeath.Die();
            // instantiate corpse
            Instantiate(corpsePrefab, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX("Death", "Fire");
        }

    }
}
