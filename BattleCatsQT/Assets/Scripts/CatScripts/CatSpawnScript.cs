using UnityEngine;
using System.Collections;

public class CatSpawnScript : MonoBehaviour {

    public GameObject cattoSpawnPrefab;
    GameObject cat;

    public GameObject catDataObject;
    public bool nameObject;
    public GameObject[] runningCatObjects = new GameObject[2];
    PlayerStableScript playerStable;
    CatCustomiseScript catCustom;
    PlayerDetailsScript playerDetails;
    CatMovementTestScript catRunningScript;
    CatPointerScript catPointerScript;
    public bool ownCat;
    public bool friendCat;
    public bool challengerCat;
    public string catModel;
    public string catSkin;
    public string catHead;
    public string catCollar;
    public bool mainMenu;
    public bool runningGame;
    public Camera levelCam;
    string[] randNamesOwner = new string[] { "Juha", "Mark", "Joshua", "Kristina", "Branimira", "Andrew", "Kristy", "Hamid", "David", "Roland" };
    string[] randNamesCat = new string[] { "Cavatar", "Tim", "Mochi", "Sookie", "Lizi", "Roshko", "Sora" };
    Animator catAnim;

    // Use this for initialization
    void Start ()
    {
        //GameObject ownCat = Instantiate(cattoSpawnPrefab, gameObject.transform, false) as GameObject;

        if (challengerCat)
        {
            SpawnChallengerCat();
        }

        if(friendCat)
        {
            SpawnFriendCat();
        }

        catDataObject = GameObject.FindGameObjectWithTag("GameData");
        playerStable = catDataObject.GetComponent<PlayerStableScript>();
        playerDetails = catDataObject.GetComponent<PlayerDetailsScript>();
        runningCatObjects = GameObject.FindGameObjectsWithTag("CatAttachObjects");
        levelCam = GameObject.FindGameObjectWithTag("LevelCam").GetComponent<Camera>();

        if (mainMenu && ownCat)
        {
            nameObject = true;
        }

        if (ownCat)
        {
            SpawnOwnCat();
        }


    }
	
    public void SpawnFriendCat()
    {
        //Set up the filepath names for the appropriate objects


        catModel = "CatModels/CatContainer"; // + playerStable.currentCat;


        catSkin = "Skins/CatSkin00" + Random.Range(0,6); //Pick a random skin



        /*
        if (playerStable.catHeadCurrent[playerStable.currentCat] != 0)
        {
            catHead = "HeadItems00" + playerStable.catHeadCurrent[playerStable.currentCat];
        }
        if (playerStable.catCollarCurrent[playerStable.currentCat] != 0)
        {
            catCollar = "CollarItems00" + playerStable.catCollarCurrent[playerStable.currentCat];
        }
        */

        Debug.Log(catModel + catSkin + catHead + catCollar);
        //GameObject cat = Instantiate(Resources.Load(catModel, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        cat = Instantiate(Resources.Load(catModel), transform.position, transform.rotation) as GameObject;
        cat.transform.SetParent(gameObject.transform);

        catPointerScript = cat.GetComponent<CatPointerScript>();
        catCustom = catPointerScript.catPointer.GetComponent<CatCustomiseScript>();
        catCustom.catSkin = Resources.Load(catSkin) as Texture;
        catCustom.catNameObject.transform.localPosition += new Vector3(0.0f, 0.2f, 0.0f); //Name size fix

        /*
        if (skinNumber == "5")
        {
            catCustom.catHead = Resources.Load("HeadItems/HeadItems002") as GameObject;
            //catCustom.catCollar = Resources.Load("CollarItems/CollarItems001") as GameObject;
        }
        */

        catCustom.catName = randNamesCat[Random.Range(0, randNamesCat.Length)];
        catCustom.catOwner = randNamesOwner[Random.Range(0, randNamesOwner.Length)];
        catAnim = catPointerScript.catPointer.GetComponent<Animator>();
        //catAnim.runtimeAnimatorController = Resources.Load("Animators/CatChallengerMenuAnimator") as RuntimeAnimatorController;
        /*
        if (catMove)
        {
            catAnim.SetInteger("ActionMove", catMoveType);
            catAnim.SetInteger("ActionNoMove", 0);
            catAnim.SetInteger("Idle", 0);
        }

        if (catFail)
        {
            catAnim.SetInteger("ActionMove", 0);
            catAnim.SetInteger("ActionNoMove", catMoveType);
            catAnim.SetInteger("Idle", 0);
        }

        if (catIdle)
        {
            catAnim.SetInteger("ActionMove", 0);
            catAnim.SetInteger("ActionNoMove", 0);
            catAnim.SetInteger("Idle", catMoveType);
        }
        */
        cat = CatScripts(catPointerScript.catPointer);
    }

    public void SpawnOwnCat()
    {
        //Set up the filepath names for the appropriate objects
        catModel = "CatModels/CatContainer" + playerStable.currentCat;
        Debug.Log(catModel);
        catSkin = "Skins/CatSkin00" + playerStable.catSkinCurrent[playerStable.currentCat];


        Debug.Log(catModel + catSkin + catHead + catCollar);
        //GameObject cat = Instantiate(Resources.Load(catModel, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        cat = Instantiate(Resources.Load(catModel), transform.position, transform.rotation) as GameObject;
        cat.transform.SetParent(gameObject.transform);

        catPointerScript = cat.GetComponent<CatPointerScript>();
        catCustom = catPointerScript.catPointer.GetComponent<CatCustomiseScript>();
        catCustom.catSkin = Resources.Load(catSkin) as Texture;

        if (playerStable.catHeadCurrent[playerStable.currentCat] != -1)
        {
            catHead = "HeadItems/HeadItems00" + playerStable.catHeadCurrent[playerStable.currentCat];
            catCustom.catHead = Resources.Load(catHead) as GameObject;
            Debug.Log(catHead);

        }
        if (playerStable.catCollarCurrent[playerStable.currentCat] != -1)
        {
            catCollar = "CollarItems/CollarItems00" + playerStable.catCollarCurrent[playerStable.currentCat];
            catCustom.catCollar = Resources.Load(catCollar) as GameObject;
            Debug.Log(catCollar);
        }

        catCustom.catName = playerStable.catName[playerStable.currentCat]; 
        catCustom.catOwner = playerDetails.userName;
        cat = CatScripts(catPointerScript.catPointer);


        //GameObject catInstance = Instantiate(cat, transform.position, transform.rotation) as GameObject;
        
        
        foreach (GameObject catPiece in runningCatObjects)
        {
            catPiece.transform.SetParent(cat.transform);
        }

    }

    public void SpawnChallengerCat()
    {
        //Set up the filepath names for the appropriate objects
        catModel = "CatModels/CatContainer"; // + playerStable.currentCat;
 
        catSkin = "Skins/CatSkin00" + Random.Range(0,5); //Pick a random skin

        /*
        if (playerStable.catHeadCurrent[playerStable.currentCat] != 0)
        {
            catHead = "HeadItems00" + playerStable.catHeadCurrent[playerStable.currentCat];
        }
        if (playerStable.catCollarCurrent[playerStable.currentCat] != 0)
        {
            catCollar = "CollarItems00" + playerStable.catCollarCurrent[playerStable.currentCat];
        }
        */

        Debug.Log(catModel + catSkin + catHead + catCollar);
        //GameObject cat = Instantiate(Resources.Load(catModel, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        cat = Instantiate(Resources.Load(catModel), transform.position, transform.rotation) as GameObject;
        cat.transform.SetParent(gameObject.transform);

        catPointerScript = cat.GetComponent<CatPointerScript>();
        catCustom = catPointerScript.catPointer.GetComponent<CatCustomiseScript>();
        catCustom.catSkin = Resources.Load(catSkin) as Texture;
        catCustom.catNameObject.transform.localPosition += new Vector3(0.0f, 0.2f, 0.0f); //Name size fix

        catCustom.catName = randNamesCat[Random.Range(0, randNamesCat.Length)];
        catCustom.catOwner = randNamesOwner[Random.Range(0, randNamesOwner.Length)];
        catAnim = catPointerScript.catPointer.GetComponent<Animator>();
        catAnim.runtimeAnimatorController = Resources.Load("Animators/CatChallengerMenuAnimator") as RuntimeAnimatorController;
        cat = CatScripts(catPointerScript.catPointer);


        //GameObject catInstance = Instantiate(cat, transform.position, transform.rotation) as GameObject;



    }

    GameObject CatScripts(GameObject cat)
    {
        catRunningScript = cat.GetComponent<CatMovementTestScript>();
        if (runningGame)
        {
            catRunningScript.enabled = true;
            levelCam.transform.SetParent(cat.transform);

        }
        else
        {
            catRunningScript.enabled = false;
        }



        return cat;
    }

    public void DeleteCat()
    {
        Destroy(cat);
    }
}
