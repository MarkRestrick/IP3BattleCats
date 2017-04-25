using UnityEngine;
using System.Collections;

public class PlayerDetailsScript : MonoBehaviour {

    public int softCurrency;
    public int hardCurrency;
    public int score;
    public string userID;
    public string userName;
    public int tutorial;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	

}
