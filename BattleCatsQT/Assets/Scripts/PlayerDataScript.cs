using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Need this for canvas

public class PlayerDataScript : MonoBehaviour {

    public int successfulHits = 0;
    public int unsuccessfulHits = 0;
    public int totalHits = 0;
    public int setHits = 0;
    public float successRate;
    public Text scoreText;
    public Text comboText;
    public Text multiplierText;
    public float speedMultiplier = 1f;
    public int playerScore = 0;
    public int playerCombo = 0;
    public int multiplier;
    public int allSuccessfulHits = 0;
    public int allUnsuccessfulHits = 0;
    public int setSuccessfulHits = 0;
    public int setUnsuccessfulHits = 0;
    public int allHits = 0;
    public float allSuccessRate;
    public float setSuccessRate;
    public int numPieces = 0; //Number of game pieces in current set
    public int numProceed = 0; //Number of pieces required to advance in current set

	
	// Update is called once per frame
	void Update ()
    {
        totalHits = successfulHits + unsuccessfulHits;
        allHits = allSuccessfulHits + allUnsuccessfulHits;
        setHits = setSuccessfulHits + setUnsuccessfulHits;

        if(totalHits > 0)
        {
            successRate = (100f / totalHits) * successfulHits;
        }
        if(allHits > 0)
        {
            allSuccessRate = (100f / allHits) * allSuccessfulHits;
        }
        if(setHits > 0)
        {
            setSuccessRate = (100f / setHits) * setSuccessfulHits;
        }

        

        
        multiplier = (playerCombo >= 20 && playerCombo < 40) ? 2 : 1;
        multiplier = (playerCombo >= 40 && playerCombo < 60) ? 3 : multiplier;
        multiplier = (playerCombo >= 60) ? 4 : multiplier;


        scoreText.text = ((int)playerScore).ToString();
        comboText.text = ((int)playerCombo).ToString();
        multiplierText.text = ((int)multiplier).ToString();



    }
}
