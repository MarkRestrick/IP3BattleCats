using UnityEngine;
using System.Collections;

public class MenuCameraScript : MonoBehaviour
{
    public GameObject camTarget;
    public GameObject camTarChallengerCat;
    public GameObject camTarOwnCat;
    public GameObject camTarRoomCenter;
    
    private Vector3 RotateToTarget;

    public float camZoomInFOV = 30f;
    public float camZoomOutFOV = 60f;

    public bool cameraZoomed;

    void Start ()
    {
        camTarget = camTarRoomCenter;
        cameraZoomed = false;
        //transform.rotation = Quaternion.LookRotation(camTarget.transform.position);
    }

    public void CameraZoomToggle()  // DOES NOT WORK CURRENTLY
    {
        if (!cameraZoomed)
        {
            StartCoroutine(CamZoomStart());
            //Camera.current.fieldOfView = camZoomInFOV;
            cameraZoomed = true;
        }

        else if (cameraZoomed)
        {
            StartCoroutine(CamZoomEnd());
            //Camera.current.fieldOfView = camZoomOutFOV;
            cameraZoomed = false;
        }
    }


    IEnumerator CamZoomStart()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime * 4;
            Camera.current.fieldOfView = Mathf.Lerp(60, 25, timeSinceStarted);
           
            // If the object has arrived, stop the coroutine
            if (Camera.current.fieldOfView == 25)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;

            //http://stackoverflow.com/questions/27212426/slowly-moving-an-object-to-a-new-position-in-unity-c-sharp
        }
    }

    IEnumerator CamZoomEnd()
    {
        float timeSinceStartedEnd = 0f;
        while (true)
        {
            timeSinceStartedEnd += Time.deltaTime * 2;
            Camera.current.fieldOfView = Mathf.Lerp(25, 60, timeSinceStartedEnd);
             
            // If the object has arrived, stop the coroutine
            if (Camera.current.fieldOfView == 60)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }




    void Update()
    {
        

        // Other tries below

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, camTarget.transform.rotation, Time.deltaTime * 40);

        /*RotateToTarget = (camTarget.transform.position);
        RotateToTarget.y = 0;  // Was in example, don't know if needed
        var rotate = Quaternion.LookRotation(RotateToTarget - transform.localPosition);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotate, Time.deltaTime * 40);
        */

    }

}
