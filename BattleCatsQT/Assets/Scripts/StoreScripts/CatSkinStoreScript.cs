using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatSkinStoreScript : MonoBehaviour {

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
    void Start ()
    {
        theButton = gameObject.GetComponent<Button>();
        theButton.onClick.AddListener(delegate { SkinUnlockOrEquip(catNumber, skinNumber); } );
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        catSpawner = GameObject.FindGameObjectWithTag("MainMenuSpawner");
        catSpawn = catSpawner.GetComponent<CatSpawnScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();


    }

    void Update()
    {
        catNumber = playerStable.currentCat;
    }
	


    public void SkinUnlockOrEquip(int catNumber, int skinNumber)
    {
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();

        Debug.Log(playerInv.catSkins[skinNumber]);

        int skinStatus = playerInv.catSkins[skinNumber]; //Grab the status of the skin

        if(skinStatus == 1) //If we own it
        {
            playerStable.catSkinCurrent[catNumber] = skinNumber; //Equip it
            catSpawn.DeleteCat();
            catSpawn.SpawnOwnCat();
            //Code to refresh cat here
        }

        if(skinStatus == 0)
        {
            if(playerDetails.softCurrency >= 500) //If we can afford it
            {
                playerDetails.softCurrency -= 500; //Buy it
                playerInv.catSkins[skinNumber] = 1; //Register it in inventory
                SkinUnlockOrEquip(catNumber, skinNumber);
            }
        }

    }

}
