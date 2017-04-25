using UnityEngine;
using System.Collections;

public class PlayGuestButtonScript : MonoBehaviour {

    GameObject gameDataObject;
    GameOptionsScript gameOptions;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;

	// Use this for initialization
	void Start ()
    {
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        gameOptions = gameDataObject.GetComponent<GameOptionsScript>();
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void GuestDetails()
    {
        //Setup the player details
        //playerDetails.softCurrency = 5000;
        //playerDetails.hardCurrency = 2;
        playerDetails.userID = "Guest";
        playerDetails.userName = "Guest1";

        //Set up the guest inventory
        /*
        playerInv.catsUnlocked[0] = 1;
        playerInv.catSkins[0] = 1;
        playerInv.catHeadItems[0] = 0;
        playerInv.catCollarItems[0] = 0;
        */

        //Set up the guest stable
        playerStable.currentCat = 0;
        playerStable.catName[0] = "Mittens";
        playerStable.catSkinCurrent[0] = 9;
        playerStable.catHeadCurrent[0] = 9;
        playerStable.catCollarCurrent[0] = 9;
    }
}