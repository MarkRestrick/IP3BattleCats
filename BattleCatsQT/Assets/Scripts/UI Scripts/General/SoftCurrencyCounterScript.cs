using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SoftCurrencyCounterScript : MonoBehaviour {

    GameObject gameOptionsObject;
    PlayerDetailsScript gameOptions;
    Text currencyText;
    

    // Use this for initialization
    void Start ()
    {
        gameOptionsObject = GameObject.FindGameObjectWithTag("GameData"); //Grab the object that contains the options data
        gameOptions = gameOptionsObject.GetComponent<PlayerDetailsScript>(); //Grab the cat data
        currencyText = GetComponent<Text>();
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        gameOptions = gameOptionsObject.GetComponent<PlayerDetailsScript>(); //Grab the cat data
        currencyText.text = gameOptions.softCurrency.ToString();

    }
}
