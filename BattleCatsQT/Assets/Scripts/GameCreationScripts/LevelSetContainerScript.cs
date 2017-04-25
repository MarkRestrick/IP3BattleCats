using UnityEngine;
using System.Collections;

public class LevelSetContainerScript : MonoBehaviour {

    public GameObject[] buttonTypes;
    public int[] pieceType;
    public float[] pieceSpawnTime;
    public Vector3[] pieceLocation;
    public Vector3[] pieceRotation;
    ObjectActivateScript objectActivate;
    public GameObject gameTimerObject;
    PlayerDataScript playerData;
    public GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    public float diffMod; //Multiplier for game difficulty


    // Use this for initialization
    void Start ()
    {
        gameOptionsObject = GameObject.FindGameObjectWithTag("GameOptions"); //Grab the object that contains the options data
        gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>(); //Grab the game data
        gameTimerObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the options data
        playerData = gameTimerObject.GetComponent<PlayerDataScript>(); //Grab the game data

        diffMod = 0.5f;

        playerData.numPieces = pieceSpawnTime.Length + 1;
        playerData.numProceed = Mathf.RoundToInt(playerData.numPieces * 0.5f);



        //Loop for each object in the set
        for (int i = pieceSpawnTime.Length -1; i >= 0; i--)
        {
            GameObject levelSet = Instantiate(buttonTypes[pieceType[i]], gameObject.transform, false) as GameObject;
            //levelSet.transform.SetParent(gameObject.transform, false); 
            //levelSet.transform.localScale = new Vector3(1, 1, 1);
            levelSet.transform.localPosition = pieceLocation[i];
            objectActivate = levelSet.GetComponent<ObjectActivateScript>();
            objectActivate.spawnTimer = pieceSpawnTime[i];
            levelSet.transform.localEulerAngles = pieceRotation[i];
            

        }
	
	}
	

}
