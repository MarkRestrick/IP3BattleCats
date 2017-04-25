using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatCollarStoreScript : MonoBehaviour
{

    public bool menuOpen = false;
    GameObject gameDataObject;
    GameObject catSpawner;
    GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    Cat1DetailsScript cat1Details;
    public int catNumber;
    public int collarNumber;
    Button theButton;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;
    CatSpawnScript catSpawn;

    // Use this for initialization
    void Start()
    {
        theButton = gameObject.GetComponent<Button>();
        theButton.onClick.AddListener(delegate { CollarUnlockOrEquip(catNumber, collarNumber); });
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        catSpawner = GameObject.FindGameObjectWithTag("MainMenuSpawner");
        catSpawn = catSpawner.GetComponent<CatSpawnScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();


    }

    void Update()
    {
        catNumber = playerStable.currentCat;
    }

    public void CollarUnlockOrEquip(int catNumber, int collarNumber)
    {
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();

        Debug.Log(playerInv.catCollarItems[collarNumber]);

        int skinStatus = playerInv.catCollarItems[collarNumber]; //Grab the status of the skin

        if (skinStatus == 1) //If we own it
        {
            playerStable.catCollarCurrent[catNumber] = collarNumber; //Equip it
            catSpawn.DeleteCat();
            catSpawn.SpawnOwnCat();
            //Code to refresh cat here
        }

        if (skinStatus == 0)
        {
            if (playerDetails.softCurrency >= 500) //If we can afford it
            {
                playerDetails.softCurrency -= 500; //Buy it
                playerInv.catCollarItems[collarNumber] = 1; //Register it in inventory
                CollarUnlockOrEquip(catNumber, collarNumber);
            }
        }

    }
}
