using UnityEngine;
using System.Collections;
using UnityEngine.UI; //JS

public class ObjectActivateScript : MonoBehaviour {

    GameObject timerObject;
    public GameObject linkedItem;
    public GameObject artObject;
    GameTimerScript gameTimer;
    public bool wasSpawned = false;
    public float spawnTimer = 0f;
    PlayerDataScript dataScript;
    float actualTimer; //Hidden timer value that will be used to spawn relative to the global game time

	// Use this for initialization
	void Start ()
    {
        timerObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the timer
        gameTimer = timerObject.GetComponent<GameTimerScript>();

        actualTimer = gameTimer.timer + spawnTimer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!linkedItem.activeSelf && !wasSpawned) //If the linked item is not active and never was activated
        {
            dataScript = timerObject.GetComponent<PlayerDataScript>();
            
            if (gameTimer.timer * dataScript.speedMultiplier > actualTimer)
            {
                linkedItem.SetActive(true);
                artObject.SetActive(true); //JS
                wasSpawned = true;
            }
        }

    }
}
