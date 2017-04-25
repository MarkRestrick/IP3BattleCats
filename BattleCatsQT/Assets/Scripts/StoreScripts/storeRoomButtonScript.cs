using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class storeRoomButtonScript : MonoBehaviour
{

    GameObject[] catCollarItems; //Array of the collar items
    Sprite[] buttonSprite; //Array of the button images
    GameObject storeButtonObject;
    Button storeButton;
    RoomStoreScript skinScript;
    public Image buttonImage;

    // Use this for initialization
    void Start()
    {
        //catCollarItems = Resources.LoadAll("CollarItems/", typeof(GameObject)).Cast<GameObject>().ToArray();
        buttonSprite = Resources.LoadAll("RoomButtonImages/", typeof(Sprite)).Cast<Sprite>().ToArray();

        for (int i = 0; i < buttonSprite.Length; i++)
        {


            storeButtonObject = Instantiate(Resources.Load("StorePrefabs/roomButton"), transform.position, transform.rotation) as GameObject;
            storeButtonObject.transform.SetParent(gameObject.transform);
            storeButton = storeButtonObject.GetComponent<Button>();
            storeButton.image.overrideSprite = buttonSprite[i];
            skinScript = storeButtonObject.GetComponent<RoomStoreScript>();
            skinScript.catNumber = 0;
            skinScript.catNumber = i;
            //skinScript = storeButtonObject.AddComponent(typeof(CatSkinStoreScript)) as CatSkinStoreScript;

        }

    }


}
