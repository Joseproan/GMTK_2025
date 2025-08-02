using System.Collections;
using UnityEngine;

public class LightingCorpse : MonoBehaviour
{
    public float pushForce = 2f;
    public float pushDuration = 0.2f; // tiempo que tarda en empujar

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDir = (collision.transform.position - transform.position).normalized;
            Vector3 targetPos = collision.transform.position + (Vector3)(pushDir * pushForce);
            StartCoroutine(PushPlayerSmoothly(collision.transform, targetPos));
        }
    }

    private IEnumerator PushPlayerSmoothly(Transform player, Vector3 targetPos)
    {
        float elapsed = 0f;
        Vector3 startPos = player.position;

        while (elapsed < pushDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pushDuration;
            player.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        player.position = targetPos; // asegurarse que llega al final
    }
}
