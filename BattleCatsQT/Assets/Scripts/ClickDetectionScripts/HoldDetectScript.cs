using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldDetectScript : MonoBehaviour, IPointerUpHandler
{

    ClickDetectScript clickDetect;
    RaycastHit hit;
    RaycastHit hit2;
    public Camera uiCam;
    public bool isHeld;
    public bool startedChecks = false;
    public Vector2 touchPosition1;
    public Vector2 touchPosition2;
    public bool doubleTouch;
    public int touchID;
    public InputManagerScript inputManagerScript;

    // Use this for initialization
    void Start()
    {
        uiCam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>(); //Grab the object that contains the timer
        clickDetect = GetComponent<ClickDetectScript>();
    }

    // Update is called once per frame
    void Update()
    {
        doubleTouch = false;



        /* /// Removed with input manager
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (i == 0)
            {
                Touch touch = Input.GetTouch(i);
                touchPosition1 = touch.position;
            }
            if (i == 1)
            {
                Touch touch2 = Input.GetTouch(i);
                touchPosition2 = touch2.position;
                doubleTouch = true;
            }

        }
        */



        if (clickDetect.wasClicked) //If it has been clicked and we have not yet started checking it
        {
            isHeld = true;
            startedChecks = true;
        }

        if (isHeld) //If the object is being held
        {

            //isHeld = false;

            /*
            Ray myRay = uiCam.ScreenPointToRay(touchPosition1);


            if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.name);

                if (hit.transform == gameObject.transform) // If the object the mouse is the correct object
                {
                    isHeld = true;
                    touchID = Input.GetTouch(0).fingerId;
                }

            }


            if (!isHeld && doubleTouch) //If we got this far, and isHeld is false (The first raycheck failed), and doubleTouch was triggered
            {
                Ray myRay2 = uiCam.ScreenPointToRay(touchPosition2);

                if (Physics.Raycast(myRay2, out hit2, Mathf.Infinity))
                {
                    Debug.Log(hit2.transform.name);

                    if (hit2.transform == gameObject.transform) // If the object the mouse is currently over is the correct object
                    {
                        isHeld = true;
                        touchID = Input.GetTouch(1).fingerId;
                    }




                }

                

            }
            */

            for (int i = 0; i < inputManagerScript.touchedObject.Length; i++)
            {
                if (inputManagerScript.touchedObject[i] != null)
                {

                    if (inputManagerScript.touchedObject[i].gameObject == gameObject)
                    {
                        isHeld = true;
                    }

                }


            }

            if (startedChecks && !isHeld) //If we've started checking, but now isHeld is false
            {
                // Debug.Log("We let it go too soon!");
            }

            isHeld = true;

        }



        GetComponent<ClickDetectScript>(); //Update the values of clickDetect



    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    void OnMouseDown()
    {
        //isHeld = true;
        //startedChecks = true;
    }
}
