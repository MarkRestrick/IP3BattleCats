using UnityEngine;
using System.Collections;


public class FacebookLogin : MonoBehaviour
{

    GameObject gameDataObject;
    GameOptionsScript gameOptions;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;

    // Use this for initialization
    void Start()
    {
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        gameOptions = gameDataObject.GetComponent<GameOptionsScript>();
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GuestDetails()
    {
        //Setup the player details
        playerDetails.softCurrency = 5000;
        playerDetails.hardCurrency = 1000;


        //Set up the guest inventory
        playerInv.catsUnlocked[0] = 1;
        playerInv.catSkins[2] = 1;
        playerInv.catHeadItems[0] = 0;
        playerInv.catCollarItems[0] = 0;

        //Set up the guest stable
        playerStable.currentCat = 0;
        playerStable.catName[0] = "Mittens";
        playerStable.catSkinCurrent[0] = 2;
        playerStable.catHeadCurrent[0] = 0;
        playerStable.catCollarCurrent[0] = 0;
    }
}