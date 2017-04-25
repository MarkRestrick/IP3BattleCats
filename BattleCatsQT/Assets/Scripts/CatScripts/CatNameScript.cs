using UnityEngine;
using System.Collections;

public class CatNameScript : MonoBehaviour {

    public Camera levelCam;

	// Use this for initialization
	void Start ()
    {
        levelCam = GameObject.FindGameObjectWithTag("LevelCam").GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(levelCam.transform);
	
	}
}
