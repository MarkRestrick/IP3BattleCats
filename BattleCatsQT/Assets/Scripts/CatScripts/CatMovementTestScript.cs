using UnityEngine;
using System.Collections;

public class CatMovementTestScript : MonoBehaviour {

    public bool[] catCheckpoint = new bool[7];

    public GameObject[] checkpointObject = new GameObject[7];

    public GameObject gameManagerObject;
    public GameManagerScript gameManager;
    public GameObject catModel; //JS
    Animator catAnimator; //JS

    public bool weMovedThisFrame = false;
    public float moveSpeed = 0.1f;
    public bool positionReached = true;

    int catAnimNumber; //JS
    int catInterest; //JS
    int playerCatInspiration; //JS

	// Use this for initialization
	void Start ()
    {
        //catCheckpointA = true;
        gameManagerObject = GameObject.FindGameObjectWithTag("RunningManager");
        gameManager = gameManagerObject.GetComponent<GameManagerScript>();

        for (int i = 0; i < 7; i++)
        {
            string checkName = "Checkpoint" + i;
            Debug.Log(checkName);
            checkpointObject[i] = GameObject.Find(checkName);

        }

    }

    // Update is called once per frame
    void Update ()
    {


        //gameManager = gameManagerObject.GetComponent<GameManagerScript>();

        if (gameManager.advance)
        {
            positionReached = false;
            CatAnimationControl(true, 1); //Starts cat moving animation
            gameManager.advance = false;
            CatAdvance();
            catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
                                                            // NOTE that now changes moving animation during movement
                                                            // Can be fixed with animator Exit time length or by code(?)
            //catAnimator.SetBool("Walking", true); //JS Old animation walk control
        }

        
        for (int i = (catCheckpoint.Length - 1); i >= 0; i--) // Iterate through the boolean array from the top
        {
            
            if (catCheckpoint[i] == true && !weMovedThisFrame) //If the one we're iterating on is true, and it's not the last one
            {

                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, checkpointObject[i+1].transform.position, step);
                if(transform.position == checkpointObject[i+1].transform.position && !positionReached)
                {
                    positionReached = true;
                    //catAnimator.SetBool("Walking", false); //Old animation walk end control
                    CatAnimationControl(false, 1); //Starts cat idle animation
                }
                weMovedThisFrame = true;
            }
        }

        weMovedThisFrame = false;
        //catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
        //catAnimator.SetBool("Walking", false); //JS <- DOES NOT WORK


        /*

        GameObject checkpointA = GameObject.Find("CheckpointA");
        GameObject checkpointB = GameObject.Find("CheckpointB");
    
        if (catCheckpointA)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, checkpointB.transform.position, step);
                if (transform.position == checkpointB.transform.position)
                {
                catCheckpointA = false;
                catCheckpointB = true;
                }
        }
        else if (catCheckpointB)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, checkpointA.transform.position, step);
            if (transform.position == checkpointA.transform.position)
            {
                catCheckpointB = false;
                catCheckpointA = true;
            }
        }
        */

    }

    public void CatAnimationControl(bool action, int moving) //JS bool action to start actions, moving 1 for movement, moving 0 for non-advancing actions
    {
        /* HOW ANIMATOR WORKS:
         * Currently the idea is that playerCatInspiration would push the animations on better tiers
         * 1-3 being Idles 1: uninterested cat, 2: medium, 3: interested
         * 11-13 Non-advancing actions with low player performance - i.e. player dropping in level sets
         * 21-23 Advancing actions with medium player performance - i.e. player staying at same level sets
         * 31-32 Advancing actions with good player performance - i.e. player going up in level sets
         * 
         * No matter how the numbers are formed, but animator will now follow these numbers.
         * After each movement cycle cat will return to similar catInterest idle. 1: uninterested, 2: sitting, 3: excited (buttwiggle atm)
         */

        if (action && moving == 1)
        {
            catInterest = Random.Range(1, 4); // needs to be non-random
            playerCatInspiration = Random.Range(2, 4)*10; // to get 20 or 30 starting animations - needs to be non-random
            catAnimNumber = catInterest + playerCatInspiration; 
            catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
            catAnimator.SetInteger("ActionMove", catAnimNumber); //
            catAnimator.SetInteger("ActionNoMove", 0); //Reset
            catAnimator.SetInteger("Idle", 0); // To prevent idle animation from playing
        }

        else if (action && moving == 0)
        {
            catInterest = Random.Range(1, 3); // needs to be non-random
            playerCatInspiration = 10; // to get 10 starting animations - needs to be non-random
            catAnimNumber = catInterest + playerCatInspiration;
            catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
            catAnimator.SetInteger("ActionNoMove", catAnimNumber);
            catAnimator.SetInteger("ActionMove", 0); //Reset
            catAnimator.SetInteger("Idle", 0); // To prevent idle animation from playing
        }


        else if (!action)
        {
            catInterest = Random.Range(1, 3);
            catAnimNumber = catInterest; // Only cat interest is required for idles
            catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
            catAnimator.SetInteger("ActionNoMove", 0); // To prevent moving animation from playing
            catAnimator.SetInteger("ActionMove", 0); //Reset
            catAnimator.SetInteger("Idle", catAnimNumber); 
            catAnimator.SetTrigger("IdleStart"); // Start idle animation transition
        }

        else //RESET EVERYTHING CAUSE EVERYTHING IS DOOMED
        {
            catAnimator = catModel.GetComponent<Animator>(); //JS Grab the animator for the feedback object
            catAnimator.SetInteger("ActionNoMove", 0); //Reset
            catAnimator.SetInteger("ActionMove", 0); //Reset
            catAnimator.SetInteger("Idle", 1); //Reset
            catAnimator.SetTrigger("IdleEmergency"); // Backup plan when everything fails
        }
    }


    public void CatAdvance()
    {


        for(int i = catCheckpoint.Length - 1; i >= 0; i --) // Iterate through the boolean array from the top
        {
            if(catCheckpoint[i] == true && i < catCheckpoint.Length - 1) //If the one we're iterating on is true, and it's not the last one
            {
                catCheckpoint[i + 1] = true; //Make the one above this true 
            }
            if(i == 0)
            {
                catCheckpoint[0] = true;
            }
        }
    }
}
