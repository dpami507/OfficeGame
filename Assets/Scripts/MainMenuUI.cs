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
        Instantiate(uiToEnable, transform);
        // activate functions in other places when game is paused
        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        FindFirstObjectByType<GameManager>().gameRunning = false;
    }

    public void ResumeGame(GameObject uiToDisable)
    {
        Destroy(uiToDisable);
        // activate functions in other places when game is resumed

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        FindFirstObjectByType<GameManager>().gameRunning = true;
        FindFirstObjectByType<PlayerManager>().CheckLevelAgain();
    }

}
