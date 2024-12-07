using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public bool isPaused = false;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(GameObject uiToEnable)
    {
        Instantiate(uiToEnable, transform);
        isPaused = true;

        // activate functions in other places when game is paused
        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 0;

        System.GC.Collect();
        FindFirstObjectByType<GameManager>().gameRunning = false;
    }

    public void ResumeGame(GameObject uiToDisable)
    {
        Destroy(uiToDisable);
        isPaused = false;
        // activate functions in other places when game is resumed

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 1;
        FindFirstObjectByType<GameManager>().gameRunning = true;
        FindFirstObjectByType<PlayerManager>().CheckLevelAgain();
    }

}
