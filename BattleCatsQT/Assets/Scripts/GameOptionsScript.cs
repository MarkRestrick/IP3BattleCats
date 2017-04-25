using UnityEngine;
using System.Collections;

public class GameOptionsScript : MonoBehaviour {

    public int gameDifficulty = 1; //Int to represent game difficulty, 1 = easy, 2 = medium, 3 = hard
    public int gameMode = 1; //Int to represent the selected game mode
    public int playerSoftCash; //The soft currency the player possesses
    public int playerHardCash; //The hard currency the player possesses
    public int catSkin; //The type of skin on the cat

    public bool returningFromGamePlay; // JS Use on MainMenu Canvas to load stuff when player returns from gameplay to mainmenu

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
	}
	

}
