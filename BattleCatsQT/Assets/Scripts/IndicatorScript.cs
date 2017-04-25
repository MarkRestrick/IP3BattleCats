using UnityEngine;
using System.Collections;

public class IndicatorScript : MonoBehaviour {
    public PlayerDataScript playerData;

    public int offsetInterval;
    public int offsetAmount;
    public int startXPos = 0;
    int totalXAxis = 805;
    
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(playerData.numPieces > 0)
        {
            offsetInterval = Mathf.RoundToInt(totalXAxis / playerData.numPieces);
            offsetAmount = playerData.successfulHits - playerData.unsuccessfulHits;
            Vector3 Position = new Vector3(offsetAmount * offsetInterval, 58, 0);
            gameObject.transform.localPosition = Position;
        }

	
	}
}
