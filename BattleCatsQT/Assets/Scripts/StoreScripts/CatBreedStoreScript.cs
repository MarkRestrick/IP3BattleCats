using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatBreedStoreScript : MonoBehaviour
{

    public bool menuOpen = false;
    GameObject gameDataObject;
    GameObject catSpawner;
    GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    Cat1DetailsScript cat1Details;
    public int catNumber;
    public int skinNumber;
    Button theButton;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;
    CatSpawnScript catSpawn;

    // Use this for initialization
    void Start()
    {
        theButton = gameObject.GetComponent<Button>();
        theButton.onClick.AddListener(delegate { SkinUnlockOrEquip(catNumber, skinNumber); });
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        catSpawner = GameObject.FindGameObjectWithTag("MainMenuSpawner");
        catSpawn = catSpawner.GetComponent<CatSpawnScript>();


    }



    public void SkinUnlockOrEquip(int catNumber, int skinNumber)
    {
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();

        Debug.Log(playerInv.catSkins[skinNumber]);

        int skinStatus = playerInv.catsUnlocked[catNumber]; //Grab the status of the skin

        if (skinStatus == 1) //If we own it
        {
            playerStable.currentCat = catNumber; //Equip it
            catSpawn.DeleteCat();
            catSpawn.SpawnOwnCat();
            //Code to refresh cat here
        }

        if (skinStatus == 0)
        {
            if (playerDetails.hardCurrency >= 500) //If we can afford it
            {
                playerDetails.hardCurrency -= 500; //Buy it
                playerInv.catsUnlocked[catNumber] = 1; //Register it in inventory
                SkinUnlockOrEquip(catNumber, skinNumber);
            }
        }

    }

}
