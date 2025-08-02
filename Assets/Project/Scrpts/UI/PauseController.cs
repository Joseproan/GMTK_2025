using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseUI;
    [HideInInspector] public bool gamePause;

    private void Awake()
    {
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        if (InputManager.PauseWasPressed && !gamePause)
        {
            gamePause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ResumeButton()
    {
        gamePause = false;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
