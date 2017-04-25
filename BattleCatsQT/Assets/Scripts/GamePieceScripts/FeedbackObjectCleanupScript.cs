using UnityEngine;
using System.Collections;

public class FeedbackObjectCleanupScript : MonoBehaviour {

    public float timeToCleanup = 1f;
    public GameObject theParent;

	// The purpose of this script is to tidy up GamePieces after a piece of feedback art has been displayed (Hit, Perfect, Miss, etc)
	void Start ()
    {
        StartCoroutine(TidyUp(timeToCleanup)); // Start the coroutine, passing in the time to actually pull the trigger

    }
	

    
    IEnumerator TidyUp(float timer)
    {
        yield return new WaitForSeconds(timeToCleanup); //Wait howeverlong is specified in the cleanup time variable
        Destroy(theParent); // Get rid of the parent, and all associated objects
    }
    
}
