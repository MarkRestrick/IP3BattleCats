using UnityEngine;
using System.Collections;

public class Cat1CustMenuScripts : MonoBehaviour {

    public bool menuOpen = false;
    public GameObject thisMenu;
    public GameObject otherMenu1;
    public Cat1CustMenuScripts otherMenuScript1;
    public GameObject otherMenu2;
    public Cat1CustMenuScripts otherMenuScript2;
    public GameObject otherMenu3;
    public Cat1CustMenuScripts otherMenuScript3;
    public GameObject otherMenu4;
    public Cat1CustMenuScripts otherMenuScript4;
    GameObject gameOptionsObject;
    GameOptionsScript gameOptions;
    Cat1DetailsScript cat1Details;

    public GameObject[] catSkinButtons;

	// Use this for initialization
	void Start ()
    {
        gameOptionsObject = GameObject.FindGameObjectWithTag("GameOptions"); //Grab the object that contains the options data
    }

    public void OpenCloseMenu()
    {
        if(menuOpen)
        {
            /*foreach(GameObject skinButton in catSkinButtons)
            {
                skinButton.SetActive(false);
            }*/
            thisMenu.SetActive(false);
            menuOpen = false;
        }
        else
        {
            //Close the other menus
            otherMenu1.SetActive(false);
            otherMenuScript1.menuOpen = false;

            otherMenu2.SetActive(false);
            otherMenuScript2.menuOpen = false;

            otherMenu3.SetActive(false);
            otherMenuScript3.menuOpen = false;

            otherMenu4.SetActive(false);
            otherMenuScript4.menuOpen = false;

            /*
            foreach (GameObject skinButton in catSkinButtons)
            {
                skinButton.SetActive(true);
            }*/
            thisMenu.SetActive(true);
            menuOpen = true;
        }
    }

    public void Skin1UnlockOrEquip()
    {
        cat1Details = gameOptionsObject.GetComponent<Cat1DetailsScript>();
        if(!cat1Details.cat1SkinEquipped[0] && cat1Details.cat1SkinsUnlocked[0]) //If we own the skin and it's not equipped
        {
            //First set all skins to be unequipped
            for(int i = 0; i < cat1Details.cat1SkinEquipped.Length; i++)
            {
                cat1Details.cat1SkinEquipped[i] = false;
            }

            //Then Set the appropriate one to be equipped
            cat1Details.cat1SkinEquipped[0] = true;
        }

        if(!cat1Details.cat1SkinsUnlocked[0])
        {
            gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>();
            if(gameOptions.playerSoftCash >= 500)
            {
                cat1Details.cat1SkinsUnlocked[0] = true;
                gameOptions.playerSoftCash -= 500;
            }
        }

    }

    public void SkinUnlockOrEquip(int skinNumber)
    {

    }

    public void Skin2UnlockOrEquip()
    {

        cat1Details = gameOptionsObject.GetComponent<Cat1DetailsScript>();
        if (!cat1Details.cat1SkinEquipped[1] && cat1Details.cat1SkinsUnlocked[1]) //If we own the skin and it's not equipped
        {
            //First set all skins to be unequipped
            for (int i = 0; i < cat1Details.cat1SkinEquipped.Length; i++)
            {
                cat1Details.cat1SkinEquipped[i] = false;
            }

            //Then Set the appropriate one to be equipped
            cat1Details.cat1SkinEquipped[1] = true;
        }

        if (!cat1Details.cat1SkinsUnlocked[1])
        {
            gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>();
            if (gameOptions.playerSoftCash >= 500)
            {
                cat1Details.cat1SkinsUnlocked[1] = true;
                gameOptions.playerSoftCash -= 500;
            }
        }

    }

    public void Skin3UnlockOrEquip()
    {

        cat1Details = gameOptionsObject.GetComponent<Cat1DetailsScript>();
        if (!cat1Details.cat1SkinEquipped[2] && cat1Details.cat1SkinsUnlocked[2]) //If we own the skin and it's not equipped
        {
            //First set all skins to be unequipped
            for (int i = 0; i < cat1Details.cat1SkinEquipped.Length; i++)
            {
                cat1Details.cat1SkinEquipped[i] = false;
            }

            //Then Set the appropriate one to be equipped
            cat1Details.cat1SkinEquipped[2] = true;
        }

        if (!cat1Details.cat1SkinsUnlocked[2])
        {
            gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>();
            if (gameOptions.playerSoftCash >= 500)
            {
                cat1Details.cat1SkinsUnlocked[2] = true;
                gameOptions.playerSoftCash -= 500;
            }
        }

    }

    public void Skin4UnlockOrEquip()
    {

        cat1Details = gameOptionsObject.GetComponent<Cat1DetailsScript>();
        if (!cat1Details.cat1SkinEquipped[3] && cat1Details.cat1SkinsUnlocked[3]) //If we own the skin and it's not equipped
        {
            //First set all skins to be unequipped
            for (int i = 0; i < cat1Details.cat1SkinEquipped.Length; i++)
            {
                cat1Details.cat1SkinEquipped[i] = false;
            }

            //Then Set the appropriate one to be equipped
            cat1Details.cat1SkinEquipped[3] = true;
        }

        if (!cat1Details.cat1SkinsUnlocked[3])
        {
            gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>();
            if (gameOptions.playerSoftCash >= 500)
            {
                cat1Details.cat1SkinsUnlocked[3] = true;
                gameOptions.playerSoftCash -= 500;
            }
        }

    }

    public void Skin5UnlockOrEquip()
    {

        cat1Details = gameOptionsObject.GetComponent<Cat1DetailsScript>();
        if (!cat1Details.cat1SkinEquipped[4] && cat1Details.cat1SkinsUnlocked[4]) //If we own the skin and it's not equipped
        {
            //First set all skins to be unequipped
            for (int i = 0; i < cat1Details.cat1SkinEquipped.Length; i++)
            {
                cat1Details.cat1SkinEquipped[i] = false;
            }

            //Then Set the appropriate one to be equipped
            cat1Details.cat1SkinEquipped[4] = true;
        }

        if (!cat1Details.cat1SkinsUnlocked[4])
        {
            gameOptions = gameOptionsObject.GetComponent<GameOptionsScript>();
            if (gameOptions.playerSoftCash >= 500)
            {
                cat1Details.cat1SkinsUnlocked[4] = true;
                gameOptions.playerSoftCash -= 500;
            }
        }

    }


    // Update is called once per frame
    void Update () {
	
	}
}
