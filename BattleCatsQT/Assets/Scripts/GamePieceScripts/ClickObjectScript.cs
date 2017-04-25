using UnityEngine;
using System.Collections;

public class ClickObjectScript : MonoBehaviour {

    public float timeToDie = 4f;
    public float timeToClick = 3f;

    //These represent the base time for a "Perfect" hit
    public float baseTimeToPerfectLow = 2.3f;
    public float baseTimeToPerfectHigh = 2.6f;

    //This represents the base time for a "Good" hit before the perfect window
    public float baseTimeToGoodBeforeLow = 2.0f;

    //This represents the base time for "Good" hit after the perfect window
    public float baseTimeToGoodAfterHigh = 2.9f;

    //This array represents the speed modifier that will be applied to all timing operations, dependant on the level
    public float[] speedMods = { 1f, 0.92f, 0.84f, 0.76f, 0.68f, 0.6f, 0.52f, 0.54f };

    PlayerDataScript playerData;
    GameObject dataObject;
    GameTimerScript gameTimer;
    ClickDetectScript clickDetect;
    float timeSpawned;
    public GameObject theParent;
    public GameObject feedbackObject;
    public GameObject feedbackExploObject;
    public GameObject audioObject;
    public Animator buttonAnimator;
    Animator feedbackAnimator;
    Animator feedbackExploAnimator;
    float realTimeSpawned;
    public GameObject theSet;
    public string theSetName;
    public char theSetValue;
    public int setNumber = 4;

    public int scoreMultiplier;

    public float speedMod;

    public float animMod = 1.75f;

    // Use this for initialization
    void Start ()
    {
        timeToDie /= animMod;
        timeToClick /= animMod;
        baseTimeToPerfectLow /= animMod;
        baseTimeToPerfectHigh /= animMod;
        baseTimeToGoodBeforeLow /= animMod;
        baseTimeToGoodAfterHigh /= animMod;


        clickDetect = GetComponent<ClickDetectScript>(); // Grab the click detection attached to this object
        dataObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the data
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data
        gameTimer = dataObject.GetComponent<GameTimerScript>(); //Grab timer data
        timeSpawned = gameTimer.timer; //Sync object timer to game time
        feedbackAnimator = feedbackObject.GetComponent<Animator>();
        feedbackExploAnimator = feedbackExploObject.GetComponent<Animator>();
        realTimeSpawned = gameTimer.timer;

        //The following lines establish which level difficulty this button belongs to based on naming conventions
        //and then assigns the appropriate speedMod
        theSet = theParent.transform.parent.gameObject;
        theSetName = theSet.transform.name;
        if(theSetName != "GameManagerObject")
        {
            theSetValue = theSetName[5];
            setNumber = int.Parse(theSetValue.ToString());
            speedMod = speedMods[setNumber - 1];
        }
        else
        {
            speedMod = 1f;
        }
        

        

        timeToDie *= speedMod;
        timeToClick *= speedMod;
        baseTimeToGoodAfterHigh *= speedMod;
        baseTimeToGoodBeforeLow *= speedMod;
        baseTimeToPerfectHigh *= speedMod;
        baseTimeToPerfectLow *= speedMod;

        buttonAnimator.speed = (1 / speedMod);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameTimer = dataObject.GetComponent<GameTimerScript>(); // update timer
        clickDetect = GetComponent<ClickDetectScript>(); // Grab the click detection attached to this object
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data

        scoreMultiplier = playerData.multiplier;


        if (clickDetect.wasClicked) //If it has been clicked
        {
            //This is where we do stuff after the object has been clicked
            playerData.successfulHits++; //Log successful hit in data
                                         /* JS Needs to instantiate HitFeedbackArt prefab at parent's transform and set animator's trigger "Bad", "Good" or "Perfect", or "Miss"
                                          * Tried to be smart and set trigger here while HitFeedbackArt was child to this object, of course it got destroyed :~(
                                          */
                                         // Debug.Log("We clicked it reddit");
            playerData.allSuccessfulHits++;
            playerData.setSuccessfulHits++;


            if (gameTimer.timer - realTimeSpawned >= baseTimeToPerfectLow && gameTimer.timer - realTimeSpawned <= baseTimeToPerfectHigh)
            {
                feedbackObject.SetActive(true); //Activate the feedback object
                feedbackExploObject.SetActive(true);
                audioObject.SetActive(true);
                feedbackAnimator.SetTrigger("Perfect");
                feedbackExploAnimator.SetTrigger("Perfect");
                playerData.playerCombo++;
                playerData.playerScore += (400 * scoreMultiplier);
                gameObject.SetActive(false); //Deactivate the target object
            }
            else if(gameTimer.timer - realTimeSpawned > baseTimeToGoodBeforeLow & gameTimer.timer - realTimeSpawned <  baseTimeToPerfectLow
                || gameTimer.timer - realTimeSpawned >  baseTimeToPerfectHigh && gameTimer.timer - realTimeSpawned <  baseTimeToGoodAfterHigh)
            {
                feedbackObject.SetActive(true); //Activate the feedback object
                feedbackExploObject.SetActive(true);
                feedbackAnimator.SetTrigger("Good");
                feedbackExploAnimator.SetTrigger("Good");
                audioObject.SetActive(true);
                playerData.playerCombo++;
                playerData.playerScore += (200 * scoreMultiplier);
                gameObject.SetActive(false); //Deactivate the target object
            }
            else
            {
                feedbackObject.SetActive(true); //Activate the feedback object
                feedbackExploObject.SetActive(true);
                feedbackAnimator.SetTrigger("Bad");
                feedbackExploAnimator.SetTrigger("Bad");
                audioObject.SetActive(true);
                playerData.playerCombo = 0;
                playerData.playerScore += (50 * scoreMultiplier);
                gameObject.SetActive(false); //Deactivate the target object
            }



            //It's likely we need to pass some data to the feedbackObject before setting it to false, we'll want the feedbackObject to delete the parent when it's done being all flashy
        }

        if (gameTimer.timer > timeSpawned + timeToDie) //If the object has outlasted its live timer
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data
                                           /* JS Again needs to instantiate HitFeedbackArt with trigger "Miss"
                                            * */
                                           //Debug.Log("Object should die here");
            playerData.allUnsuccessfulHits++;
            playerData.setUnsuccessfulHits++;
            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            feedbackAnimator.SetTrigger("Miss");
            feedbackExploAnimator.SetTrigger("Miss");
            playerData.playerCombo = 0;
            gameObject.SetActive(false); //Deactivate the target object
        }

        if(gameTimer.timer > timeSpawned + timeToClick)
        {
            //Shouldn't actually need this check, should be handled inside the "Was clicked" part, but hey!
            //Debug.Log("THIS WAS THE CLICK MOMENT");
        }

    }
}
