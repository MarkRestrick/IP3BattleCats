using UnityEngine;
using System.Collections;

public class Cat1CustomiseScript : MonoBehaviour {

    Cat1DetailsScript catOptions;
    GameObject gameOptionsObject;
    public GameObject theCat;
    public Texture[] catTextures;
    public int catSkin = 0;
    Renderer catRend;

	// Use this for initialization
	void Start ()
    {

        CatSkin();

    }
	

    public void CatSkin()
    {

        gameOptionsObject = GameObject.FindGameObjectWithTag("GameOptions"); //Grab the object that contains the options data
        catOptions = gameOptionsObject.GetComponent<Cat1DetailsScript>(); //Grab the cat data
        catRend = theCat.GetComponent<Renderer>();

        for (int i = 0; i < catOptions.cat1SkinEquipped.Length; i++)
        {
            if (catOptions.cat1SkinEquipped[i])
            {
                catSkin = i;
            }
        }
        catRend.material.mainTexture = (catTextures[catSkin]);

    }

}
