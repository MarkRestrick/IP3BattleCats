using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickDetectScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool wasClicked;
    RaycastHit hit;
    RaycastHit hit2;
    public Camera uiCam;
    public int pointerID;
    Vector2 touchPosition1;
    Vector2 touchPosition2;
    bool doubleTouch;
    public bool touchDetect;
    public InputManagerScript inputManagerScript;

    void Start()
    {
        uiCam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>(); //Grab the object that contains the timer
        inputManagerScript = GameObject.FindGameObjectWithTag("Input").GetComponent<InputManagerScript>(); //Grab the object that contains the input manager
    }


    // Update is called once per frame
    void Update()
    {

        bool foundIt = false;
        for(int i = 0; i < inputManagerScript.touchedObject.Length; i++)
        {
            if(inputManagerScript.touchedObject[i] != null)
            {

                    if (inputManagerScript.touchedObject[i].gameObject == gameObject)
                    {
                        foundIt = true;
                    }

            }


        }
        if(foundIt == true)
        {
            wasClicked = true;
        }
        else
        {
            wasClicked = false;
        }

        /*
        if (touchDetect)
        {

            if (!wasClicked) //If the object has not been clicked
            {

                for (int i = 0; i < Input.touchCount; i++) //Iterate through the touches and grab data from the first 2
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

            }




            Ray myRay = uiCam.ScreenPointToRay(touchPosition1);


            if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
            {


                if (hit.transform == gameObject.transform) // If the player is touching the correct object
                {
                    wasClicked = true;
                }

            }



            if (!wasClicked && doubleTouch) //If we got this far, and wasClicked is false (The first raycheck failed), and doubleTouch was triggered
            {

                Ray myRay2 = uiCam.ScreenPointToRay(touchPosition2);

                if (Physics.Raycast(myRay2, out hit2, Mathf.Infinity))
                {


                    if (hit2.transform == gameObject.transform) // If the object the mouse is currently over is not the proper game Object
                    {
                        wasClicked = true;

                    }

                }


            }

        }*/
    }


    public void GotClicked()
    {
        //wasClicked = true;
    }


    public void OnPointerDown(PointerEventData touchData)
    {

        /*
        Ray myRay = uiCam.ScreenPointToRay(touchData.position); // Create a ray from camera to mouse position


        if (Physics.Raycast(myRay, out hit, Mathf.Infinity)) //Analyse the raycast hits
        {
            Debug.Log("Passed first bit");

            if (hit.transform == gameObject.transform)
            {
                wasClicked = true;
            }

        }
        */

        touchDetect = true;

        //wasClicked = true;
    }

    public void OnPointerUp(PointerEventData touchData)
    {
        touchDetect = false;
        wasClicked = false;

    }







    void OnMouseDown()
    {
        //wasClicked = true;
    }

}
