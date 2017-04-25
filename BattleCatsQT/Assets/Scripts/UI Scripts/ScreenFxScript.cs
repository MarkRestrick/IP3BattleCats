using UnityEngine;
using System.Collections;

public class ScreenFxScript : MonoBehaviour {

    PlayerDataScript playerData;
    GameObject dataObject;

    Animator screenFXAnimator; //JS
    float comboAnimFloat; //JS


    // Use this for initialization
    void Start ()
    {
        dataObject = GameObject.FindGameObjectWithTag("Timer"); //Grab the object that contains the data
        playerData = dataObject.GetComponent<PlayerDataScript>(); //Grab player data
        screenFXAnimator = GetComponent<Animator>(); //JS
    }

    // Update is called once per frame
    void Update () {

        comboAnimFloat = playerData.playerCombo; //JS Convert Int to Float for animation blending purposes
        screenFXAnimator.SetFloat("ComboBlend", comboAnimFloat); //JS

    }
}
