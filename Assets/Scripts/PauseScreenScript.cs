using UnityEngine;

public class PauseScreenScript : MonoBehaviour
{
    public bool isActive = false;

    private void Update()
    {
        //transform.localScale = new Vector3(Mathf.Clamp(((float)Screen.width / 800), 0.05f, 1), Mathf.Clamp(((float)Screen.height / 600), 0.05f, 1), 1);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        isActive = true;
        // activate functions in other places when game is paused
        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 0;
        FindFirstObjectByType<GameManager>().gameRunning = false;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        isActive = false;
        // activate functions in other places when game is resumed

        // TIME SCALE MUST BE LAST OR THE GAME SOFTLOCKS
        Time.timeScale = 1;
        FindFirstObjectByType<GameManager>().gameRunning = true;
        FindFirstObjectByType<PlayerManager>().CheckLevelAgain();
    }
}
