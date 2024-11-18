using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public int kills;
    public int level;

    public TMP_Text levelText;
    public TMP_Text killsText;
    public TMP_Text deathMsgText;

    public GameObject board;

    public string[] deathMsgs;

    public bool showing;

    private void Start()
    {
        showing = false;

        deathMsgText.text = deathMsgs[Random.Range(0, deathMsgs.Length)];
    }

    private void Update()
    {
        if(showing)
            board.SetActive(true);
        else 
            board.SetActive(false);

        ShowScreen();
    }

    public void ShowScreen()
    {
        levelText.text = level.ToString();
        killsText.text = kills.ToString();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
