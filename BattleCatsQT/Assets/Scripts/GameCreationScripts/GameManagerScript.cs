using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public GameObject[] level1Sets;
    public GameObject[] level2Sets;
    public GameObject[] level3Sets;
    public GameObject[] level4Sets;
    public GameObject[] level5Sets;
    public GameObject[] level6Sets;
    public GameObject[] level7Sets;
    public GameObject[] level8Sets;


    GameObject gameData;
    PlayerDetailsScript playerDets;
    GameTimerScript gameTimer;
    PlayerDataScript playerData;
    GameOptionsScript gameOptions;

    public GameObject gameCompleteObject;
    public Text gameCompleteText;
    public GameObject playerDataObject;
    public GameObject gameTimerObject;
    public GameObject gameOptionsObject;
    public int currentDifficulty = 2;
    public int numberOfGameSections = 6;
    public int sectionsPlayed = 0;
    public float sectionTimer;
    public int chosenSection = 0;
    public float gameStartDelay;
    public float easySuccessTarget = 95f;
    public float mediumSuccessTarget = 85f;
    public float hardSuccessTarget = 70f;
    public float lastSectionSuccessRate = 0f;

    public bool advance;

    public GameObject levelCompletePanel;
    public Text levelEndScoreText;
    public Text levelEndPerfectsText;
    public Text levelEndGoodsText;
    public Text levelEndBadsText;
    public Text levelEndMissesText;
    public Text levelEndRewardText;

    Animator catAnimator; //JS

    // Use this for initialization
    void Start ()
    {
        gameOptionsObject = GameObject.FindGameObjectWithTag("GameOptions"); //Grab the object that contains the options data
        gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>(); //Grab the game data

        gameData = GameObject.FindGameObjectWithTag("GameData"); //Grab the object that contains the options data
        playerDets = gameData.GetComponent<PlayerDetailsScript>();

        StartCoroutine(StartTheGame());






        
        
	}
	


    void GameEnd()
    {
        playerData = playerDataObject.GetComponent<PlayerDataScript>();
        gameCompleteObject.SetActive(true);
        gameCompleteText = gameCompleteObject.GetComponent<Text>();
        int successAmount = (int) playerData.allSuccessRate;
        int levelScore = (int) playerData.playerScore; //JS
        int levelHits = (int) playerData.allSuccessfulHits; //JS
        int levelMisses = (int)playerData.allUnsuccessfulHits; //JS
        int levelAllHits = (int)playerData.allHits; //JS
        float missAmount = (100f / levelAllHits ) * levelMisses; //JS
        //gameCompleteText.text = ("Congratulations! You completed the event with " + successAmount.ToString() + "% accuracy and have earned " + successAmount.ToString() + " coins!");
        gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>(); //Grab the game data
        gameOptions.playerSoftCash += successAmount;
        playerDets.softCurrency += successAmount;
        playerDets.score += levelScore;
        levelCompletePanel.SetActive(true); //JS
        levelEndScoreText.text = levelScore.ToString(); //JS
        levelEndPerfectsText.text = (levelHits.ToString() + "(" + successAmount.ToString() + "%)"); //JS
        levelEndMissesText.text = (levelMisses.ToString() + "(" + missAmount.ToString() + "%)"); //JS
        levelEndRewardText.text = (successAmount.ToString());

        // Cat needs to go to trigger Win here

    }

    void GenerateLevel(int currentDifficulty, int chosenSection)
    {
        sectionsPlayed++; //Increase the number of sections played counter


        if (sectionsPlayed <= numberOfGameSections) //If we haven't maxed out on game sections
        {

            switch (currentDifficulty)
            {


                case 1:
                    GameObject levelSet = Instantiate(level1Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 2:
                    levelSet = Instantiate(level2Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 3:
                    levelSet = Instantiate(level3Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 4:
                    levelSet = Instantiate(level4Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 5:
                    levelSet = Instantiate(level5Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 6:
                    levelSet = Instantiate(level6Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 7:
                    levelSet = Instantiate(level7Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
                case 8:
                    levelSet = Instantiate(level8Sets[chosenSection], transform.position, transform.rotation) as GameObject;
                    levelSet.transform.SetParent(gameObject.transform);
                    levelSet.transform.localScale = new Vector3(1, 1, 1);
                    StartCoroutine(SectionDelay());
                    break;
            }
        }
        else
        {
            GameEnd();
        }







    }



    void GenerateNextSetDetails(int gameDifficulty)
    {
        playerData = playerDataObject.GetComponent<PlayerDataScript>();

        if (gameDifficulty == 1)
        {

            //Evaluate player's success relative to the target for this difficulty
            if (playerData.setSuccessRate > easySuccessTarget)
            {
                currentDifficulty++;
                if((playerData.setSuccessRate + lastSectionSuccessRate)/2 > easySuccessTarget)
                {
                    currentDifficulty++;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty > 8) //Protect against going out of range
                {
                    currentDifficulty = 8;
                }
            }
            if (playerData.setSuccessRate < (easySuccessTarget - 5))
            {
                currentDifficulty--;
                if ((playerData.setSuccessRate + lastSectionSuccessRate) / 2 < easySuccessTarget && lastSectionSuccessRate != 0)
                {
                    currentDifficulty--;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty < 1) //Protect against going out of range
                {
                    currentDifficulty = 1;
                }
            }



            //Next, grab a level set number based on how many exist for the chosen difficulty
            switch (currentDifficulty)
            {
                case 1:
                    chosenSection = Random.Range(0, level1Sets.Length);
                    break;
                case 2:
                    chosenSection = Random.Range(0, level2Sets.Length);
                    break;
                case 3:
                    chosenSection = Random.Range(0, level3Sets.Length);
                    break;
                case 4:
                    chosenSection = Random.Range(0, level4Sets.Length);
                    break;
                case 5:
                    chosenSection = Random.Range(0, level5Sets.Length);
                    break;
                case 6:
                    chosenSection = Random.Range(0, level6Sets.Length);
                    break;
                case 7:
                    chosenSection = Random.Range(0, level7Sets.Length);
                    break;
                case 8:
                    chosenSection = Random.Range(0, level8Sets.Length);
                    break;
            }


        }

        if (gameDifficulty == 2)
        {

            //Evaluate player's success relative to the target for this difficulty
            if (playerData.setSuccessRate > (mediumSuccessTarget + 5))
            {
                currentDifficulty++;
                if ((playerData.setSuccessRate + lastSectionSuccessRate) / 2 > mediumSuccessTarget)
                {
                    currentDifficulty++;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty > 8) //Protect against going out of range
                {
                    currentDifficulty = 8;
                }
            }
            if (playerData.setSuccessRate < (mediumSuccessTarget - 10))
            {
                currentDifficulty--;
                if ((playerData.setSuccessRate + lastSectionSuccessRate) / 2 < mediumSuccessTarget && lastSectionSuccessRate != 0)
                {
                    currentDifficulty--;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty < 1) //Protect against going out of range
                {
                    currentDifficulty = 1;
                }
            }



            //Next, grab a level set number based on how many exist for the chosen difficulty
            switch (currentDifficulty)
            {
                case 1:
                    chosenSection = Random.Range(0, level1Sets.Length);
                    break;
                case 2:
                    chosenSection = Random.Range(0, level2Sets.Length);
                    break;
                case 3:
                    chosenSection = Random.Range(0, level3Sets.Length);
                    break;
                case 4:
                    chosenSection = Random.Range(0, level4Sets.Length);
                    break;
                case 5:
                    chosenSection = Random.Range(0, level5Sets.Length);
                    break;
                case 6:
                    chosenSection = Random.Range(0, level6Sets.Length);
                    break;
                case 7:
                    chosenSection = Random.Range(0, level7Sets.Length);
                    break;
                case 8:
                    chosenSection = Random.Range(0, level8Sets.Length);
                    break;
            }


        }


        if (gameDifficulty == 3)
        {

            //Evaluate player's success relative to the target for this difficulty
            if (playerData.setSuccessRate > hardSuccessTarget + 10)
            {
                currentDifficulty++;
                if ((playerData.setSuccessRate + lastSectionSuccessRate) / 2 > hardSuccessTarget)
                {
                    currentDifficulty++;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty > 8) //Protect against going out of range
                {
                    currentDifficulty = 8;
                }
            }
            if (playerData.setSuccessRate < (hardSuccessTarget - 10))
            {
                currentDifficulty--;
                if ((playerData.setSuccessRate + lastSectionSuccessRate) / 2 < hardSuccessTarget && lastSectionSuccessRate != 0)
                {
                    currentDifficulty--;
                }
                lastSectionSuccessRate = playerData.setSuccessRate;
                if (currentDifficulty < 1) //Protect against going out of range
                {
                    currentDifficulty = 1;
                }
            }



            //Next, grab a level set number based on how many exist for the chosen difficulty
            switch (currentDifficulty)
            {
                case 1:
                    chosenSection = Random.Range(0, level1Sets.Length);
                    break;
                case 2:
                    chosenSection = Random.Range(0, level2Sets.Length);
                    break;
                case 3:
                    chosenSection = Random.Range(0, level3Sets.Length);
                    break;
                case 4:
                    chosenSection = Random.Range(0, level4Sets.Length);
                    break;
                case 5:
                    chosenSection = Random.Range(0, level5Sets.Length);
                    break;
                case 6:
                    chosenSection = Random.Range(0, level6Sets.Length);
                    break;
                case 7:
                    chosenSection = Random.Range(0, level7Sets.Length);
                    break;
                case 8:
                    chosenSection = Random.Range(0, level8Sets.Length);
                    break;
            }


        }

        playerData.setSuccessfulHits = 0;
        playerData.setUnsuccessfulHits = 0;
    }

    void EvaluateAndSpawnNext(int currentDifficulty, int chosenSection)
    {

        playerData = playerDataObject.GetComponent<PlayerDataScript>();
        if(playerData.successRate < 50f)
        {
            playerData.successfulHits = 0;
            playerData.unsuccessfulHits = 0;
            
        }
        else
        {
            advance = true;
            playerData.successfulHits = 0;
            playerData.unsuccessfulHits = 0;
        }
        GenerateLevel(currentDifficulty, chosenSection);
    }

    IEnumerator SectionDelay()
    {

        yield return new WaitForSeconds(sectionTimer);
        GenerateNextSetDetails(gameOptions.gameDifficulty);
        EvaluateAndSpawnNext(currentDifficulty, chosenSection);
    }

    IEnumerator StartTheGame()
    {
        //The following statements detect the appropriate level of difficulty to start the game on, based on the
        //game options object
        currentDifficulty = (gameOptions.gameDifficulty == 1) ? 2 : 4;
        if (gameOptions.gameDifficulty == 3)
        {
            currentDifficulty = 6;
        }

        //After this, we need to establish which of the pieces from the appropriate set we will use

        if (currentDifficulty == 2)
        {
            chosenSection = Random.Range(0, level2Sets.Length);
        }
        if (currentDifficulty == 4)
        {
            chosenSection = Random.Range(0, level4Sets.Length);
        }
        if (currentDifficulty == 6)
        {
            chosenSection = Random.Range(0, level6Sets.Length);
        }

        yield return new WaitForSeconds(gameStartDelay);

        //Now that we have established the appropriate starting block, we can call the GenerateLevel function with
        //Our difficulty level and set section
        GenerateLevel(currentDifficulty, chosenSection);

    }
}
