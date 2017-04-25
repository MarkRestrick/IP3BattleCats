using UnityEngine;
using System.Collections;

public class GameTimerScript : MonoBehaviour {

    //bool timerActive = false;
    public float timer = 0f;

	// Use this for initialization
	void Start ()
    {
        //timerActive = true;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
	
	}
}
