using UnityEngine;
using System.Collections;

public class IndicatorLerpScript : MonoBehaviour {

    public Transform targetPos;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (targetPos.position != this.transform.position)
        {
            //this.transform.LookAt(targetPos);
            this.transform.position = Vector3.Lerp(transform.position, targetPos.position, Time.deltaTime * speed);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }
}
