using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;

    public static bool gamePaused = false;
    private static bool clickTilePause = false;

    public void Pause()
    {
        gamePaused = true;
        if (ClickTile.pause)
        {
            clickTilePause = true;
        }
        ClickTile.pause = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        gamePaused = false;
        if (!clickTilePause)
        {
            ClickTile.pause = false;
        }
        Time.timeScale = 1;
        clickTilePause = false;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
        Continue();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Continue();
    }
}