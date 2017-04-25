using UnityEngine;
using System.Collections;

public class MenuButtonAudioScript : MonoBehaviour {

    AudioSource theAudio;

	// Use this for initialization
	void Start ()
    {
        theAudio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void PlayAudio()
    {
        theAudio.Play();
    }
}
