using UnityEngine;
using System.Collections;

public class HoldObjectScript : MonoBehaviour
{

    //This array represents the speed modifier that will be applied to all timing operations, dependant on the level
    public float[] speedMods = { 1f, 0.92f, 0.84f, 0.76f, 0.68f, 0.6f, 0.52f, 0.54f };

    public float timeToDie = 5f;
    public float timeToClick = 3f;
    public float timeToHold = 1f;
    public float baseTimeForGood = 0.5f;
    public float timeHeld = 0f;
    PlayerDataScript playerData;
    GameObject dataObject;
    GameTimerScript gameTimer;
    ClickDetectScript clickDetect;
    HoldDetectScript holdDetect;
    float timeSpawned;
    public GameObject theParent;
    public bool wasHeld = false;
    public GameObject animHolder;
    Animator holdAnim;
    public GameObject feedbackObject;
    public GameObject feedbackExploObject;
    public GameObject audioObject;
    Animator feedbackAnimator;
    Animator feedbackExploAnimator;
    public bool startedPlaying;
    public GameObject theSet;
    public string theSetName;
    public char theSetValue;
    public int setNumber;

    public int scoreMultiplier;
    public float speedMod;

    // Use this for initialization
    void Start()
    {
        clickDetect = GetComponent<ClickDetectScript>(); // Grab the click detection attached to this object
        holdDetect = GetComponent<HoldDetectScript>(); // Grab the hold detection attached to this object
        dataObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the data
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data
        gameTimer = dataObject.GetComponent<GameTimerScript>(); //Grab timer data
        timeSpawned = gameTimer.timer; //Sync object timer to game time
        holdAnim = animHolder.GetComponent<Animator>();
        feedbackAnimator = feedbackObject.GetComponent<Animator>();
        feedbackExploAnimator = feedbackExploObject.GetComponent<Animator>();

        //The following lines establish which level difficulty this button belongs to based on naming conventions
        //and then assigns the appropriate speedMod
        theSet = theParent.transform.parent.gameObject;
        theSetName = theSet.transform.name;
        if (theSetName != "GameManagerObject")
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
        timeToHold *= speedMod;
        baseTimeForGood *= speedMod;

        holdAnim.speed = (1 / speedMod);

    }

    // Update is called once per frame
    void Update()
    {
        gameTimer = dataObject.GetComponent<GameTimerScript>(); // update timer
        clickDetect = GetComponent<ClickDetectScript>(); // Grab the click detection attached to this object
        holdDetect = GetComponent<HoldDetectScript>(); // Grab the hold detection attached to this object
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data

        scoreMultiplier = playerData.multiplier;

        if (clickDetect.wasClicked && holdDetect.isHeld) //If it has been clicked and is being held down
        {
            wasHeld = true; //Logs that the object has been hit
            timeHeld += Time.deltaTime;

            if (startedPlaying)
            {
                holdAnim.speed = (1 / speedMod);
                audioObject.SetActive(true);
            }
            if (!startedPlaying)
            {
                holdAnim.SetBool("Held", true);
                startedPlaying = true;
                audioObject.SetActive(true);
            }



        }

        if (!clickDetect.wasClicked && wasHeld) //JS If it has been clicked and is released
        {
            holdAnim.speed = 0; //JS
            audioObject.SetActive(false);
            clickDetect.touchDetect = false;


        }


        /*

        if(wasHeld && !holdDetect.isHeld) // If the player held over, but released
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data

            //Debug.Log("Object should die here");
            Destroy(theParent);
        }
        */



        if (gameTimer.timer > timeSpawned + timeToDie && timeHeld == 0f) //If the object has outlasted its live timer and it was never charged
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data
            playerData.allUnsuccessfulHits++;
            playerData.setUnsuccessfulHits++;

            //Debug.Log("Object should die here");
            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            feedbackExploAnimator.SetTrigger("Miss");
            audioObject.SetActive(false);
            feedbackAnimator.SetTrigger("Miss");
            playerData.playerCombo = 0;

            gameObject.SetActive(false); //Deactivate the target object
        }

        if (gameTimer.timer > timeSpawned + timeToDie && timeHeld > 0f && timeHeld <= baseTimeForGood) //If the object has outlasted its live but it got charged a bit
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data
            playerData.allUnsuccessfulHits++;
            playerData.setUnsuccessfulHits++;

            //Debug.Log("Object should die here");
            feedbackObject.SetActive(true); //Activate the feedback object
            audioObject.SetActive(false);
            feedbackExploObject.SetActive(true);
            feedbackExploAnimator.SetTrigger("Bad");
            feedbackAnimator.SetTrigger("Bad");
            playerData.playerCombo = 0;
            playerData.playerScore += (50 * scoreMultiplier);
            gameObject.SetActive(false); //Deactivate the target object
        }

        if (gameTimer.timer > timeSpawned + timeToDie && timeHeld > baseTimeForGood) //If the object has outlasted its live but it got charged a lot
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.successfulHits++; //Log failed hit in data
            playerData.allSuccessfulHits++;
            playerData.setSuccessfulHits++;

            //Debug.Log("Object should die here");
            feedbackObject.SetActive(true); //Activate the feedback object
            audioObject.SetActive(false);
            feedbackExploObject.SetActive(true);
            feedbackExploAnimator.SetTrigger("Good");
            feedbackAnimator.SetTrigger("Good");
            playerData.playerCombo++;
            playerData.playerScore += (200 * scoreMultiplier);
            gameObject.SetActive(false); //Deactivate the target object
        }

        if (timeHeld >= timeToHold) //If it has been held long enough
        {
            //This is where we do stuff after the object has been clicked
            playerData.successfulHits++; //Log successful hit in data
                                         // Debug.Log("We clicked it reddit");
            playerData.allSuccessfulHits++;
            playerData.setSuccessfulHits++;
            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            feedbackExploAnimator.SetTrigger("Perfect");
            feedbackAnimator.SetTrigger("Perfect");
            audioObject.SetActive(false);
            playerData.playerCombo++;
            playerData.playerScore += (400 * scoreMultiplier);
            gameObject.SetActive(false); //Deactivate the target object
        }

        if (gameTimer.timer > timeSpawned + timeToClick)
        {
            //Shouldn't actually need this check, should be handled inside the "Was clicked" part, but hey!
            //Debug.Log("THIS WAS THE CLICK MOMENT");
        }

    }
}
