using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UsernameMainMenuScript : MonoBehaviour {

    public GameObject gameDataObject;
    public IAPDemoSceneController IAP;
    public PlayerDetailsScript playerDets;
    public Text userText;

	// Use this for initialization
	void Start ()
    {
        gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        IAP = gameDataObject.GetComponent<IAPDemoSceneController>();
        playerDets = gameDataObject.GetComponent<PlayerDetailsScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetUser()
    {
        playerDets.userName = userText.text;
        if(playerDets.userName != null)
        {
            IAP.SetUser(playerDets.userName);
        }
        
    }
}
