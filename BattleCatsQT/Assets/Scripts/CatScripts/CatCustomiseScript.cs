using UnityEngine;
using System.Collections;

public class CatCustomiseScript : MonoBehaviour {

    GameObject gameDataObject;
    public GameObject catContainer;
    public GameObject theCat;
    public GameObject catHeadLocator;
    public GameObject catCollarLocator;
    public Texture catSkin;
    public GameObject catHead;
    public GameObject catCollar;
    public GameObject catNameObject;
    public TextMesh catText;
    PlayerStableScript playerStable;
    Renderer catRend;
    public string catName;
    public string catOwner;
    CatSpawnScript spawnDetails;
    

	// Use this for initialization
	void Start ()
    {
        //gameDataObject = GameObject.FindGameObjectWithTag("GameData");
        //playerStable = gameDataObject.GetComponent<PlayerStableScript>();
        catRend = theCat.GetComponent<Renderer>();
        catRend.material.mainTexture = catSkin;

        catText.text = catName + "\n" + "<" + catOwner + "'s cat>";
        spawnDetails = catContainer.GetComponentInParent<CatSpawnScript>();
        if(spawnDetails.ownCat)
        {
            catText.text = catName + "\n" + "<Your cat>";
        }
        if(spawnDetails.nameObject)
        {
            catNameObject.SetActive(true);
        }

        if(catHead)
        {
            GameObject catHeadActual = Instantiate(catHead, catHeadLocator.transform, false) as GameObject;
            //catHeadActual.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);
            //catHeadActual.transform.localScale *= 1.3f;
            catHeadActual.transform.SetParent(catHeadLocator.transform);
        }

        if(catCollar)
        {
            GameObject catCollarActual = Instantiate(catCollar, catCollarLocator.transform, false) as GameObject;
            //catCollarActual.transform.Rotate(0f, 90f, 0f);
            //catCollarActual.transform.localScale *= 1.3f;
            catCollarActual.transform.SetParent(catCollarLocator.transform);
        }
        
        //Instantiate(catHead, catHeadLocator.transform.position, catHeadLocator.transform.rotation);


    }
	

}
