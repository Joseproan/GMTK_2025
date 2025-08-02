using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;

    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    [SerializeField] CheckDrop checkDrop;

    private GameObject grabbedObject;

    private void Update()
    {
        if(checkDrop.detectCannon)
        {
            if(InputManager.InteractWasPressed && grabbedObject == null)
            {
                grabbedObject = checkDrop.cannon;
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }

            else if (InputManager.InteractWasPressed && checkDrop.dontDrop)
            {
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }


        }
    }
}
