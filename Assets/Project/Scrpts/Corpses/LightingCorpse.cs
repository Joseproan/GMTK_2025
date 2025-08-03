using UnityEngine;

public class LightingCorpse : MonoBehaviour
{
    GameObject[] cannons;
    GameObject[] electricCannons;
    string electricCannon = "Lightning";

    int currentIndex = 0;
    float speed = 2f; // Velocidad del lerp

    private void Start()
    {
        cannons = GameObject.FindGameObjectsWithTag("Cannon");
        electricCannons = System.Array.FindAll(cannons, cannon =>
        cannon.GetComponent<Cannon>()?.bulletTag == electricCannon);
    }

    private void Update()
    {
        if (electricCannons.Length == 0) return;

        Vector3 targetPos = electricCannons[currentIndex].transform.position;

        // Mueve hacia el objetivo a velocidad constante
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 2f)
        {
            currentIndex = (currentIndex + 1) % electricCannons.Length;
        }
    }

}
