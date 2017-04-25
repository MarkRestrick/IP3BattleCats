using UnityEngine;
using System.Collections;

public class PlayerInventoryScript : MonoBehaviour {

    public int numberOfCats = 5;
    public int numberOfSkins = 5;
    public int numberOfHead = 5;
    public int numberOfCollar = 5;
    public int numberOfRooms = 3;


    public int[] catsUnlocked;
    public int[] catSkins;
    public int[] catHeadItems;
    public int[] catCollarItems;
    public int[] catRoomItems;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        catsUnlocked = new int[numberOfCats];
        catSkins = new int[numberOfSkins];
        catHeadItems = new int[numberOfHead +1];
        catCollarItems = new int[numberOfCollar +1];
        catRoomItems = new int[numberOfRooms];

        /*
        for(int i = 0; i < numberOfCats; i++)
        {
            catSkins[i] = new int[numberOfSkins[i]];
        }
        */
    }
	

}
