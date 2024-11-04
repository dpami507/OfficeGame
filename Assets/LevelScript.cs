using UnityEngine;

public class LevelScript : MonoBehaviour
{
    private void OnEnable()
    {
        // add code to randomize 
        Debug.Log("Activated");
    }

    public void DisableSelf()
    {
        FindFirstObjectByType<MainMenuUI>().ResumeGame(gameObject);
    }

}
