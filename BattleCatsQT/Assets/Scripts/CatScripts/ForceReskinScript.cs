using UnityEngine;
using System.Collections;

public class ForceReskinScript : MonoBehaviour {

    Cat1CustomiseScript catCustomise;

	// Use this for initialization
	void Start ()
    {
        catCustomise = GetComponent<Cat1CustomiseScript>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        catCustomise.CatSkin();
	}
}
