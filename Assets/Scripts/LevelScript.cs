using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.WindowsRuntime;
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
     * 5. Sugar Cube
     * 6. Copier
     * 7. Color Ink
     */
    public List<GameObject> uiOfChoices;
    PlayerManager player;
    List<GameObject> allWeaponData;

    // Random Weapons and Trinket data
    int choiceAt = 0;
    string[] choices = new string[3];
    int[] levelsOfItems = { -1, -1, -1 };
    GameObject[] itemsToDelete = new GameObject[3];

    // potential items to get randomly
    List<string> possibleItems = new List<string>();
    List<int> correspondingLevel = new List<int>();


    // I can make this whole code way more efficient later but I just need to make sure it works first


    private void Start()
    {
        // make sure that the choices are reset
        choices = new string[3];
        choiceAt = 0;
        //possibleItems.Clear();
        //correspondingLevel.Clear();
        //itemsToDelete = new GameObject[3];

        // add code to randomize 
        player = FindFirstObjectByType<PlayerManager>();
        allWeaponData = player.weapons;
        for (int i = 0; i < choices.Length; i++) {
            choices[i] = RandomItemsAvailable();
        }
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
            if (weapon.GetComponent<WeaponBaseScript>().level <= 8 && (temp != choices[0] && temp != choices[1] && temp != choices[2]) && player.numWeapons < 6)
            {
                possibleItems.Add(temp);
                correspondingLevel.Add(weapon.GetComponent<WeaponBaseScript>().level);
            }
        }
        // get all valid trinkets that aren't already chosen as an item
        foreach (string trinket in player.TrinketData.Keys)
        {
            if (player.TrinketData[trinket] <= 5 && (trinket != choices[0] && trinket != choices[1] && trinket != choices[2]) && player.numTrinkets < 6)
            {
                possibleItems.Add(trinket);
                correspondingLevel.Add(player.TrinketData[trinket]);
            }
        }
        // if there is at least one valid item left, randomly choose a value from the list and return that item.
        if (possibleItems.Count != 0)
        {
            // debug leftover if necessary to test again
            int num = Random.Range(0, possibleItems.Count );
            levelsOfItems[choiceAt] = correspondingLevel[num];
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
        // choices setup and function assignment
        int yLevel = 65;
        for (int i = 0; i < choices.Length; i++) {
            if (choices[i] != "Skip")
            {
                GameObject uiChoice = Instantiate(ReturnGameObject(choices[i]), gameObject.transform);
                uiChoice.transform.localPosition = new Vector3(transform.position.x * 0, yLevel, transform.position.z);
                uiChoice.GetComponent<OptionInfoScript>().data = choices[i];
                uiChoice.GetComponent<OptionInfoScript>().player = player;
                uiChoice.GetComponent<OptionInfoScript>().manage = this;
                // you MUST keep this name variable the same of level ups will break for some reason lfmao
                uiChoice.GetComponent<OptionInfoScript>().name.text = 
                    choices[i] + " - " + (levelsOfItems[i] == 1 ? "New!" : levelsOfItems[i]);
                uiChoice.GetComponent<OptionInfoScript>().description.text = player.LevelDescription(choices[i], levelsOfItems[i]);
                itemsToDelete[i] = uiChoice;
                yLevel -= 80;
            }
        }
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
            case "Sugar Cube":
                return uiOfChoices[5];
            case "Copier":
                return uiOfChoices[6];
            case "Color Ink":
                return uiOfChoices[7];
            default:
                return uiOfChoices[0];
        }
    }
}
