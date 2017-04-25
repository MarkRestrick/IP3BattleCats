using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunningGameBoardScript : MonoBehaviour {

    Text catNameText;
    PlayerStableScript playerStable;
    GameObject playerDataObject;

	// Use this for initialization
	void Start ()
    {
        playerDataObject = GameObject.FindGameObjectWithTag("GameData");
        playerStable = playerDataObject.GetComponent<PlayerStableScript>();
        catNameText = gameObject.GetComponent<Text>();
        //catNameText.text = playerStable.catName[playerStable.currentCat];
    }
	
}
