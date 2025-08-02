using UnityEngine;

public class CheckDrop : MonoBehaviour
{
    public bool dontDrop,detectCannon;
    public GameObject cannon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cannon"))
        {
            // Aquí va tu lógica al colisionar con un objeto de la layer "Cannon"
            detectCannon = true;
            cannon = collision.gameObject;
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Cannon"))
        {
            // Aquí va tu lógica al colisionar con un objeto de la layer "Cannon"
            dontDrop = true;
        }
        */
        dontDrop = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cannon"))
        {
            // Aquí va tu lógica al colisionar con un objeto de la layer "Cannon"
            detectCannon = true;
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Cannon"))
        {
            // Aquí va tu lógica al colisionar con un objeto de la layer "Cannon"
            dontDrop = true;
        }*/
        dontDrop = false;
    }
}
