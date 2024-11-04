using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
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
        uiToEnable.SetActive(true);
        // activate functions in other places when game is paused

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 0;
    }

    public void ResumeGame(GameObject uiToDisable)
    {
        uiToDisable.SetActive(false);
        // activate functions in other places when game is resumed

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 1;
    }
}
