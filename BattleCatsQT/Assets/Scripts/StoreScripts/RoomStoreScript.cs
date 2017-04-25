using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomStoreScript : MonoBehaviour {

    public bool menuOpen = false;
    GameObject gameDataObject;
    GameObject roomSpawner;
    GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    
    public int catNumber;
    public int skinNumber;
    Button theButton;
    PlayerDetailsScript playerDetails;
    PlayerInventoryScript playerInv;
    PlayerStableScript playerStable;
    RoomScript roomSpawn;
    

    // Use this for initialization
    void Start()
    {
        theButton = gameObject.GetComponent<Button>();
        theButton.onClick.AddListener(delegate { SkinUnlockOrEquip(catNumber, skinNumber); });
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        roomSpawner = GameObject.FindGameObjectWithTag("RoomSpawner");
        roomSpawn = roomSpawner.GetComponent<RoomScript>();


    }



    public void SkinUnlockOrEquip(int catNumber, int skinNumber)
    {
        playerDetails = gameDataObject.GetComponent<PlayerDetailsScript>();
        playerInv = gameDataObject.GetComponent<PlayerInventoryScript>();
        playerStable = gameDataObject.GetComponent<PlayerStableScript>();

        Debug.Log(playerInv.catSkins[skinNumber]);

        int skinStatus = playerInv.catRoomItems[catNumber]; //Grab the status of the skin

        if (skinStatus == 1) //If we own it
        {
            playerStable.currentRoom = catNumber +1; //Equip it
            
            roomSpawn.SpawnRoom();
            //Code to refresh cat here
        }

        if (skinStatus == 0)
        {
            if (playerDetails.hardCurrency >= 1000) //If we can afford it
            {
                playerDetails.hardCurrency -= 1000; //Buy it
                playerInv.catRoomItems[catNumber] = 1; //Register it in inventory
                SkinUnlockOrEquip(catNumber, skinNumber);
            }
        }

    }
}
