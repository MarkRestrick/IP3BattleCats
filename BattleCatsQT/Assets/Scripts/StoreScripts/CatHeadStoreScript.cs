using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatHeadStoreScript : MonoBehaviour {

    public bool menuOpen = false;
    GameObject gameDataObject;
    GameObject catSpawner;
    GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    Cat1DetailsScript cat1Details;
    public int catNumber;
    public int headNumber;
    Button theButton;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;
    CatSpawnScript catSpawn;

    // Use this for initialization
    void Start ()
    {
        theButton = gameObject.GetComponent<Button>();
        theButton.onClick.AddListener(delegate { HeadUnlockOrEquip(catNumber, headNumber); });
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        catSpawner = GameObject.FindGameObjectWithTag("MainMenuSpawner");
        catSpawn = catSpawner.GetComponent<CatSpawnScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();


    }

    void Update()
    {
        catNumber = playerStable.currentCat;
    }
    public void HeadUnlockOrEquip(int catNumber, int headNumber)
    {
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();

        Debug.Log(playerInv.catHeadItems[headNumber]);

        int skinStatus = playerInv.catHeadItems[headNumber]; //Grab the status of the skin

        if (skinStatus == 1) //If we own it
        {
            playerStable.catHeadCurrent[catNumber] = headNumber; //Equip it
            catSpawn.DeleteCat();
            catSpawn.SpawnOwnCat();
            //Code to refresh cat here
        }

        if (skinStatus == 0)
        {
            if (playerDetails.softCurrency >= 500) //If we can afford it
            {
                playerDetails.softCurrency -= 500; //Buy it
                playerInv.catHeadItems[headNumber] = 1; //Register it in inventory
                HeadUnlockOrEquip(catNumber, headNumber);
            }
        }

    }
}
