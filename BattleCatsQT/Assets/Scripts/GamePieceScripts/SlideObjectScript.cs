using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideObjectScript : MonoBehaviour
{

    public float timeToDie = 5f;
    public float timeToClick = 3f;
    public float timeToHold = 1f;
    public float timeHeld = 0f;
    PlayerDataScript playerData;
    GameObject dataObject;
    GameTimerScript gameTimer;
    ClickDetectScript clickDetect;
    HoldDetectScript holdDetect;
    float timeSpawned;
    public GameObject theParent;
    public bool wasHeld = false;
    public bool moveFinished = false;
    public Camera gameCam;


    public GameObject audioObject;
    public GameObject startObject;
    public GameObject endObject;
    public float moveSpeed;
    public GameObject feedbackObject;
    public GameObject feedbackExploObject;
    public GameObject sliderGraphicsParent;
    Animator feedbackAnimator;
    Animator feedbackExploAnimator;
    public int touchID;
    Touch touch;
    public bool offsetSet = false;
    float touchXOffset;
    float touchYOffset;
    public bool keepGoing = false;
    public bool oncePerFrame;
    public bool grabOffset = false;
    public Vector2 contMove;
    public bool noTouch = false;
    public float lastX = 0;
    public float currentX = 0;
        
    Vector3 positionToMove;
    Vector2 pointerOffset;
    Vector2 pointerLocalPosition;

    public RectTransform theButton;
    public RectTransform gamePanel;



    public int scoreMultiplier;



    // Use this for initialization
    void Start()
    {
        clickDetect = GetComponent<ClickDetectScript>(); // Grab the click detection attached to this object
        holdDetect = GetComponent<HoldDetectScript>(); // Grab the hold detection attached to this object
        dataObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the data
        gameCam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>();
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data
        gameTimer = dataObject.GetComponent<GameTimerScript>(); //Grab timer data
        timeSpawned = gameTimer.timer; //Sync object timer to game time
        feedbackAnimator = feedbackObject.GetComponent<Animator>(); // Grab the animator for the feedback object
        feedbackExploAnimator = feedbackExploObject.GetComponent<Animator>();


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
        }

        if(keepGoing)
        {

            SlideObject();
            oncePerFrame = true;
            
        }

        if (wasHeld && !keepGoing)
        {
            if(!oncePerFrame)
            {
 
                audioObject.SetActive(true);
                SlideObject();
            }
            
        }

        if (wasHeld && !holdDetect.isHeld && !keepGoing) // If the player held over, but released
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data
            playerData.allUnsuccessfulHits++;
            playerData.setUnsuccessfulHits++;
            playerData.playerCombo = 0;
            //Debug.Log("Object should die here");

            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            audioObject.SetActive(false); //Deactivate the button sound object
            feedbackObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackExploObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackAnimator.SetTrigger("Bad");
            feedbackExploAnimator.SetTrigger("Bad");
            sliderGraphicsParent.SetActive(false); //Deactivate the target object
        }

        if (holdDetect.isHeld && clickDetect.wasClicked) //If it has been clicked and is being held down
        {
            timeHeld += Time.deltaTime;
        }

        if (gameTimer.timer > timeSpawned + timeToDie) //If the object has outlasted its live timer
        {
            //This should mostly be used to report a failed click/clean up unused objects
            playerData.unsuccessfulHits++; //Log failed hit in data
            playerData.allUnsuccessfulHits++;
            playerData.setUnsuccessfulHits++;
            playerData.playerCombo = 0;
            //Debug.Log("Object should die here");
            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            audioObject.SetActive(false); //Deactivate the button sound object
            feedbackObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackExploObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackAnimator.SetTrigger("Bad");
            feedbackExploAnimator.SetTrigger("Bad");
            sliderGraphicsParent.SetActive(false); //Deactivate the target object
        }

        if (moveFinished) //If it has been held long enough
        {
            //This is where we do stuff after the object has been clicked
            playerData.successfulHits++; //Log successful hit in data
                                         // Debug.Log("We clicked it reddit");
            playerData.allSuccessfulHits++;
            playerData.setSuccessfulHits++;
            playerData.playerCombo++;
            playerData.playerScore += (400 * scoreMultiplier);
            feedbackObject.SetActive(true); //Activate the feedback object
            feedbackExploObject.SetActive(true);
            audioObject.SetActive(false); //Deactivate the button sound object
            feedbackObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackExploObject.transform.eulerAngles = new Vector3(0, 0, -theParent.transform.rotation.z);
            feedbackAnimator.SetTrigger("Perfect");
            feedbackExploAnimator.SetTrigger("Perfect");
            sliderGraphicsParent.SetActive(false); //Deactivate the target object
        }

        if (gameTimer.timer > timeSpawned + timeToClick)
        {
            //Shouldn't actually need this check, should be handled inside the "Was clicked" part, but hey!
            //Debug.Log("THIS WAS THE CLICK MOMENT");
        }

    }

    void SlideObject()
    {

        lastX = gameObject.transform.position.x;
        /* The following is the old code for moving the slider at a fixed speed
        
        float step = moveSpeed * Time.deltaTime * playerData.speedMultiplier;
        transform.position = Vector3.MoveTowards(transform.position, endObject.transform.position, step);
        Debug.Log(transform.position);
        
        if(transform.position == endObject.transform.position)
        {
            moveFinished = true;
        }
        */

        keepGoing = true;



        holdDetect = GetComponent<HoldDetectScript>(); // Grab the hold detection attached to this object

        touchID = holdDetect.touchID; //Grab the appropriate touch ID for this object

        for (int i = 0; i < Input.touchCount; i++)
        {

            if (Input.GetTouch(i).fingerId == touchID)
            {
                touch = Input.GetTouch(i);
                
            }

        }

        if(!grabOffset)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(theButton, touch.position, gameCam, out pointerOffset);
            grabOffset = true;
        }
        Debug.Log(pointerOffset);
        /*

        if (offsetSet)
        {
            offsetOrientCounter++;
        }
        if (offsetOrientCounter > 1)
        {
            offsetSet = false;
            offsetOrientCounter = 0;
        }
        if (!offsetSet)
        {
            touchXOffset = -touch.position.x;
            touchYOffset = -touch.position.y;
            offsetSet = true;
        }


        

        //We need to read the screen movements, relative to the rotation of the game object, eg left to right we waant to match the screen
        //movements, but right to left we'd have to inverse the player input to get the appropriate behaviour
        if(theParent.transform.eulerAngles.z >=0 && theParent.transform.eulerAngles.z <= 90)
        {
            //This handles horizontal left to right, to vertical bottom to top
            positionToMove = new Vector3(((touch.position.x + touchXOffset) + (touch.position.y + touchYOffset)) / 2, 0, 0);
        }
        else if(theParent.transform.eulerAngles.z > 90 && theParent.transform.eulerAngles.z <= 180)
        {
            //This handles vertical bottom to top, to horizontal right to left
            positionToMove = new Vector3((-(touch.position.x + touchXOffset) + (touch.position.y + touchYOffset)) / 2, 0, 0);
        }
        else if (theParent.transform.eulerAngles.z > 180 && theParent.transform.eulerAngles.z <= 270)
        {
            //This handles horizontal right to left, to vertical top to bottom
            positionToMove = new Vector3((-(touch.position.x + touchXOffset) + -(touch.position.y + touchYOffset)) / 2, 0, 0);
        }
        else if (theParent.transform.eulerAngles.z > 270)
        {
            //This handles vertical top to bottom, to horizontal left to right
            positionToMove = new Vector3(((touch.position.x + touchXOffset) + -(touch.position.y + touchYOffset)) / 2, 0, 0);
        }

        //Vector3 positionToMove = new Vector3((Mathf.Abs((touch.position.x + touchXOffset)) + Mathf.Abs((touch.position.y + touchYOffset))) / 2, 0, 0);


        float step = Time.deltaTime * playerData.speedMultiplier;

        transform.localPosition += positionToMove * 0.035f;

        //gameObject.transform.localPosition = positionToMove;

    */

        pointerOffset.y = 0;


        RectTransformUtility.ScreenPointToLocalPointInRectangle(gamePanel, touch.position, gameCam, out pointerLocalPosition);

        pointerLocalPosition.y = 0;

        if(pointerLocalPosition.x - pointerOffset.x >= 0)
        {

            theButton.localPosition = pointerLocalPosition - pointerOffset;

        }

        
        
        

        

        if (transform.localPosition.x >= endObject.transform.localPosition.x)
        {
            moveFinished = true;
        }

        


        if(touch.phase == TouchPhase.Ended)
        {
            
            noTouch = true;
        }

        currentX = gameObject.transform.position.x;

        if (!holdDetect.isHeld)
        {
            theButton.localPosition += new Vector3(1, 0, 0);
        }

    }
}
