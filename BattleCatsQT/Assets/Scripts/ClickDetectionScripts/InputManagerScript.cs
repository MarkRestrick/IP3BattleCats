using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManagerScript : MonoBehaviour {

    public Text objectText;
    public Camera uiCam;
    public Transform[] touchedObject = new Transform[5];
    RaycastHit hit;

	// Use this for initialization
	void Start () {
        uiCam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>(); //Grab the object that contains the timer

    }
	
	// Update is called once per frame
	void Update ()
    {
        Touch[] myTouches = Input.touches;

        for(int i = 0; i <Input.touchCount; i++)
        {
            switch(myTouches[i].phase)
            {
                case TouchPhase.Began:
                    Ray myRay = uiCam.ScreenPointToRay(myTouches[i].position);
                    if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
                    {
                        //objectText.text = hit.transform.name;
                        touchedObject[i] = hit.transform;
                    }
                    break;

                case TouchPhase.Ended:
                    touchedObject[i] = null;
                    break;
            }

        }
	
	}
}
