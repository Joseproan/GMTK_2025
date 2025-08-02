using System.Collections;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;

    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    [SerializeField] CheckDrop checkDrop;

    private GameObject grabbedObject;

    public LayerMask cannonLayer;
    private bool isGrabbing;
    [SerializeField] private GameObject interactionCanvas;

    private void Start()
    {
        checkDrop.dontDrop = true;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.right, rayDistance, cannonLayer);


        if (hit.collider != null && hit.collider.CompareTag("Cannon") && !isGrabbing)
        {
            interactionCanvas.SetActive(true);
        }
        else
        {
            interactionCanvas.SetActive(false);
        }

        if (hit.collider != null && hit.collider.CompareTag("Cannon") && InputManager.InteractWasPressed && grabbedObject == null && !isGrabbing)
        {
            grabbedObject = hit.collider.gameObject;
            grabbedObject.GetComponent<Cannon>().enabled = false;
            grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
            grabbedObject.transform.position = grabPoint.position;
            grabbedObject.transform.SetParent(transform);
            isGrabbing = true;
            interactionCanvas.SetActive(false);
            AudioManager.Instance.PlaySFX("Player", "Grab");
        }
        else if (isGrabbing && !checkDrop.dontDrop && InputManager.InteractWasPressed)
        {
            grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
            grabbedObject.transform.SetParent(null);
            isGrabbing = false;
            StartCoroutine(EnableCannonCo());
            AudioManager.Instance.PlaySFX("Player", "Put");
        }
    }

    IEnumerator EnableCannonCo()
    {
        yield return new WaitForSeconds(1f);
        grabbedObject.GetComponent<Cannon>().enabled = true;
        grabbedObject = null;
    }
}
