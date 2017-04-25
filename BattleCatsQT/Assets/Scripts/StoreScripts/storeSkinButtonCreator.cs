using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class storeSkinButtonCreator : MonoBehaviour {

    Texture[] catSkins; //Array of the skins
    Sprite[] buttonSprite; //Array of the button images
    GameObject storeButtonObject;
    Button storeButton;
    CatSkinStoreScript skinScript;
    public Image buttonImage;
    
    

	// Use this for initialization
	void Start ()
    {
        catSkins = Resources.LoadAll("skins/", typeof(Texture)).Cast<Texture>().ToArray() ;
        buttonSprite = Resources.LoadAll("SkinButtonImages/", typeof(Sprite)).Cast<Sprite>().ToArray();


        for (int i = 0; i < buttonSprite.Length; i++)
        {
            

            storeButtonObject = Instantiate(Resources.Load("StorePrefabs/skinButton"), transform.position, transform.rotation) as GameObject;
            storeButtonObject.transform.SetParent(gameObject.transform);            
            storeButton = storeButtonObject.GetComponent<Button>();
            storeButton.image.overrideSprite = buttonSprite[i];
            skinScript = storeButtonObject.GetComponent<CatSkinStoreScript>();
            skinScript.catNumber = 0;
            skinScript.skinNumber = i;
            //skinScript = storeButtonObject.AddComponent(typeof(CatSkinStoreScript)) as CatSkinStoreScript;

        }
	
	}
	

}
