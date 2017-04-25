using UnityEngine;
using System.Collections;

public class DifficultySelectScript : MonoBehaviour {

    GameOptionsScript gameOptions;

	// Use this for initialization
	void Start ()
    {
        gameOptions = gameObject.GetComponent<GameOptionsScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectEasy()
    {
        gameOptions.gameDifficulty = 1;
    }

    public void SelectMedium()
    {
        gameOptions.gameDifficulty = 2;
    }

    public void SelectHard()
    {
        gameOptions.gameDifficulty = 3;
    }

}
