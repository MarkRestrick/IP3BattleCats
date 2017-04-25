using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class storeHeadButtonCreatorScript : MonoBehaviour {

    GameObject[] catHeadItems; //Array of the head items
    Sprite[] buttonSprite; //Array of the button images
    GameObject storeButtonObject;
    Button storeButton;
    CatHeadStoreScript skinScript;
    public Image buttonImage;

    // Use this for initialization
    void Start ()
    {
        catHeadItems = Resources.LoadAll("HeadItems/", typeof(GameObject)).Cast<GameObject>().ToArray();
        buttonSprite = Resources.LoadAll("HeadButtonImages/", typeof(Sprite)).Cast<Sprite>().ToArray();

        for (int i = 0; i < catHeadItems.Length; i++)
        {


            storeButtonObject = Instantiate(Resources.Load("StorePrefabs/headButton"), transform.position, transform.rotation) as GameObject;
            storeButtonObject.transform.SetParent(gameObject.transform);
            storeButton = storeButtonObject.GetComponent<Button>();
            storeButton.image.overrideSprite = buttonSprite[i];
            skinScript = storeButtonObject.GetComponent<CatHeadStoreScript>();
            skinScript.catNumber = 0;
            skinScript.headNumber = i;
            //skinScript = storeButtonObject.AddComponent(typeof(CatSkinStoreScript)) as CatSkinStoreScript;

        }

    }


}
