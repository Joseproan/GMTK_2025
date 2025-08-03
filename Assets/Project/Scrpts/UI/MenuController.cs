using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{

}
public class MenuController : MonoBehaviour
{
    public GameScenes sceneManager;
    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
