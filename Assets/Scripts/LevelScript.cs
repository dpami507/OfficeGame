using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    // dependencies and refrences
    /* List of all Weapon and Trinket names:
     * 1. Pencil
     * 2. Printer
     * 3. Coffee
     * 4. Magnet
     */
    public List<GameObject> uiOfChoices;
    PlayerManager player;
    List<GameObject> allWeaponData;

    // Random Weapons and Trinket data
    int choiceAt = 0;
    int[] levelsOfItems = { -1, -1, -1 };
    string choice1 = "";
    string choice2 = "";
    string choice3 = "";
    GameObject[] itemsToDelete = new GameObject[3];

    // potential items to get randomly
    List<string> possibleItems = new List<string>();
    List<int> correspondingLevel = new List<int>();


    // I can make this whole code way more efficient later but I just need to make sure it works first


    private void Start()
    {
        // make sure that the choices are reset
        choice1 = null;
        choice2 = null;
        choice3 = null;
        choiceAt = 0;
        //possibleItems.Clear();
        //correspondingLevel.Clear();
        //itemsToDelete = new GameObject[3];

        // add code to randomize 
        player = FindFirstObjectByType<PlayerManager>();
        allWeaponData = player.weapons;
        choice1 = RandomItemsAvailable();
        choice2 = RandomItemsAvailable();
        choice3 = RandomItemsAvailable();
        RevealChoices();
        Debug.Log("Activated");
    }

    public void DisableSelf()
    {
        // destroys all the options once selected in order to not have the options from this level up still there on the next one.
        foreach (GameObject ui in itemsToDelete) {
            if (ui != null) { Destroy(ui); }
        }
        FindFirstObjectByType<MainMenuUI>().ResumeGame(gameObject);
    }
    private string RandomItemsAvailable()
    {
        possibleItems.Clear();
        correspondingLevel.Clear();
        string temp;
        // get all valid weapons that aren't already chosen as an item 
        foreach (GameObject weapon in allWeaponData)
        {
            temp = weapon.GetComponent<WeaponBaseScript>().nameWeapon;
            if (weapon.GetComponent<WeaponBaseScript>().level < 8 && (temp != choice1 && temp != choice2 && temp != choice3) && player.numWeapons < 6)
            {
                possibleItems.Add(temp);
                correspondingLevel.Add(weapon.GetComponent<WeaponBaseScript>().level);
            }
        }
        // get all valid trinkets that aren't already chosen as an item
        foreach (string trinket in player.TrinketData.Keys)
        {
            if (player.TrinketData[trinket] < 8 && (trinket != choice1 && trinket != choice2 && trinket != choice3) && player.numTrinkets < 6)
            {
                possibleItems.Add(trinket);
                correspondingLevel.Add(player.TrinketData[trinket]);
            }
        }
        if (possibleItems.Count != 0)
        {
            int num = Random.Range(0, possibleItems.Count -1);
            Debug.Log(choiceAt + " - " + levelsOfItems[choiceAt] + " - " + possibleItems.Count + " - " + num);
            Debug.Log(num + " - " + correspondingLevel[num]);
            levelsOfItems[choiceAt] = 
                correspondingLevel[num];
            if (choice1 != null) {
                Debug.Log("Choice 1: " + choice1);
            }
            if (choice2 != null)
            {
                Debug.Log("Choice 2: " + choice2);
            }
            if (choice3 != null)
            {
                Debug.Log("Choice 3: " + choice3);
            }
            Debug.Log("Possible item chosen: " + possibleItems[num]);
            choiceAt++;
            return possibleItems[num];
        }
        else {
            return "Skip";
        }
    }
    private void RevealChoices()
    {
        // choice one should be at y 65, choice two should be at y -15, choice 3 should be at y -95
        // choice 1 setup and function assignment
        if (choice1 != "Skip")
        {
            GameObject uiChoice1 = Instantiate(ReturnGameObject(choice1), gameObject.transform);
            uiChoice1.transform.localPosition = new Vector3(transform.position.x * 0, 65, transform.position.z);
            uiChoice1.GetComponent<Button>().onClick.AddListener(ChoiceOne);
            uiChoice1.GetComponent<OptionInfoScript>().name.text = 
                choice1 + " - " + 
                (levelsOfItems[0] == 0 ? "New!": levelsOfItems[0] + 1);
            // add custom descriptions later
            itemsToDelete[0] = uiChoice1;
        }

        // choice 2 setup and function assignment
        if (choice2 != "Skip")
        {
            GameObject uiChoice2 = Instantiate(ReturnGameObject(choice2), gameObject.transform);
            uiChoice2.transform.localPosition = new Vector3(transform.position.x * 0, -15, transform.position.z);
            uiChoice2.GetComponent<Button>().onClick.AddListener(ChoiceTwo);
            uiChoice2.GetComponent<OptionInfoScript>().name.text = choice2 + " - " + (levelsOfItems[1] == 0 ? "New!" : levelsOfItems[1] + 1);
            itemsToDelete[1] = uiChoice2;
            // add custom descriptions later
        }

        if (choice3 != "Skip")
        {
            // choice 3 setup and function assignment
            GameObject uiChoice3 = Instantiate(ReturnGameObject(choice3), gameObject.transform);
            uiChoice3.transform.localPosition = new Vector3(transform.position.x * 0, -95, transform.position.z);
            uiChoice3.GetComponent<Button>().onClick.AddListener(ChoiceThree);
            uiChoice3.GetComponent<OptionInfoScript>().name.text = choice3 + " - " + (levelsOfItems[2] == 0 ? "New!" : levelsOfItems[2] + 1);
            itemsToDelete[2] = uiChoice3;
            // add custom descriptions later
        }
    }

    public void ChoiceOne() 
    {
        player.ApplyLevelChoice(choice1);
        DisableSelf();
    }
    public void ChoiceTwo()
    {
        player.ApplyLevelChoice(choice2);
        DisableSelf();
    }
    public void ChoiceThree()
    {
        player.ApplyLevelChoice(choice3);
        DisableSelf();
    }

    private GameObject ReturnGameObject(string name) {
        switch (name) {
            case "Pencil":
                return uiOfChoices[1];
            case "Printer":
                return uiOfChoices[2];
            case "Coffee":
                return uiOfChoices[3];
            case "Magnet":
                return uiOfChoices[4];
            default:
                return uiOfChoices[1];
        }
    }
}
