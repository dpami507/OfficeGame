using TMPro;
using UnityEngine;

public class OptionInfoScript : MonoBehaviour
{
    // for some inexplicable reason, if you change this variable's name, the game WILL break and only show one broken option on the level up screen LMAO
    public TMP_Text name;
    public TMP_Text description;
    public string data;
    public PlayerManager player;
    public LevelScript manage;

    public void Chosen() {
        player.ApplyLevelChoice(data);
        manage.DisableSelf();
    }
}
