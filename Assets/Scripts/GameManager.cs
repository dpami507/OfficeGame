using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameRunning;

    private void Start()
    {
        Application.targetFrameRate = 60;
        gameRunning = true;
    }
}
