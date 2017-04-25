using UnityEngine;
using System.Collections;

public class PlayerStableScript : MonoBehaviour {

    public int currentCat;
    public int currentRoom;
    public string[] catName;
    public int[] catSkinCurrent;
    public int[] catHeadCurrent;
    public int[] catCollarCurrent;

    PlayerInventoryScript playerInv;

	// Use this for initialization
	void Start ()
    {
        playerInv = gameObject.GetComponent<PlayerInventoryScript>();
        catName = new string[playerInv.numberOfCats];
        catSkinCurrent = new int[playerInv.numberOfCats];
        catHeadCurrent = new int[playerInv.numberOfCats];
        catCollarCurrent = new int[playerInv.numberOfCats];

    }
	

}
