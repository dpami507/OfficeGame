using UnityEngine;

public class PauseScreenScript : MonoBehaviour
{
    public bool isActive = false;
    public void PauseGame()
    {
        gameObject.SetActive(true);
        isActive = true;
        // activate functions in other places when game is paused
        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        FindFirstObjectByType<GameManager>().gameRunning = false;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        isActive = false;
        // activate functions in other places when game is resumed

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        FindFirstObjectByType<GameManager>().gameRunning = true;
        FindFirstObjectByType<PlayerManager>().CheckLevelAgain();
    }
}
