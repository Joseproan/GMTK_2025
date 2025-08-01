using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionController : MonoBehaviour
{
    public void Intro()
    {
        SceneManager.LoadScene("Intro Test");
    }

    public void NPC()
    {
        SceneManager.LoadScene("Cannon Test");
    }
}
