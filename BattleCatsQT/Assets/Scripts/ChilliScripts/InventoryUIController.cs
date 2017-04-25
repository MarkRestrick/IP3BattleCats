using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Listens for changes in the inventory and updates the UI
/// 
public class InventoryUIController : MonoBehaviour
{
    private Text m_coinText;
    private Text m_gemText;
    public PlayerDetailsScript playerDets;
    public PlayerInventoryScript playerInv;
    public PlayerStableScript playerStab;

    public int maxCats;
    public int maxSkin;
    public int maxHead;
    public int maxCollar;
    public int maxRoom;

    ///
    private void Start()
    {
        /*
        m_coinText = transform.FindChild("CoinText").GetComponent<Text>();
        m_coinText.text = "Coins: Fetching";
        m_gemText = transform.FindChild("GemText").GetComponent<Text>();
        m_gemText.text = "Gems: Fetching";
        */
        maxCats = playerInv.numberOfCats;
        maxSkin = playerInv.numberOfSkins;
        maxHead = playerInv.numberOfHead;
        maxCollar = playerInv.numberOfCollar;
        maxRoom = playerInv.numberOfRooms;
        

        InventorySystem.Get().OnInventoryUpdated += OnInventoryUpdated;

        StartCoroutine(InitialRun());

    }

    IEnumerator InitialRun()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SaveAll());
    }

    /// Called whenever the inventory is updated to refresh the UI
    /// 
    public void OnInventoryUpdated()
    {
        //Debug.Log( string.Format("Coins: {0}", InventorySystem.Get().GetAmount(InventorySystem.k_coinsId)) );

        //Retrieve player items
        playerDets.softCurrency = InventorySystem.Get().GetAmount(InventorySystem.k_coinsId); //Grab coins
        playerDets.hardCurrency = InventorySystem.Get().GetAmount(InventorySystem.k_gemsId); //Grab gems
        playerDets.score = InventorySystem.Get().GetAmount(InventorySystem.k_scoreId); //Grab gems
        playerDets.tutorial = InventorySystem.Get().GetAmount(InventorySystem.k_tutorial); //Grab gems


        for (int i = 0; i < maxCats; i++)
        {
            playerInv.catsUnlocked[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CatNum[i]);

        }

        for (int i = 0; i < maxSkin; i++)
        {
            playerInv.catSkins[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CatSkins[i]);
        }

        for (int i = 0; i < maxHead; i++)
        {
            playerInv.catHeadItems[i] = InventorySystem.Get().GetAmount(InventorySystem.k_HeadNum[i]);
        }

        for (int i = 0; i < maxCollar; i++)
        {
            playerInv.catCollarItems[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CollarNum[i]);
        }

        for (int i = 0; i < maxRoom; i++)
        {
            playerInv.catRoomItems[i] = InventorySystem.Get().GetAmount(InventorySystem.k_RoomNum[i]);
        }

        //----------------------------
        //After retrieving all of the players current items, we grab their saved cat setups

        playerStab.currentCat = InventorySystem.Get().GetAmount(InventorySystem.k_currentCat);

        playerStab.currentRoom = InventorySystem.Get().GetAmount(InventorySystem.k_currentRoom);

        for (int i = 0; i < maxCats; i++)
        {
            playerStab.catSkinCurrent[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CatSkinCurr[i]);
        }

        for (int i = 0; i < maxCats; i++)
        {
            playerStab.catHeadCurrent[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CatHeadCurr[i]);
        }

        for (int i = 0; i < maxCats; i++)
        {
            playerStab.catCollarCurrent[i] = InventorySystem.Get().GetAmount(InventorySystem.k_CatCollarCurr[i]);
        }

        //playerInv.catsUnlocked = InventorySystem.Get().GetAmount(InventorySystem.k_CatNum); //Grab gems

    }

    public void OnInventorySave()
    {
        //Debug.Log( string.Format("Coins: {0}", InventorySystem.Get().GetAmount(InventorySystem.k_coinsId)) );

        //Retrieve player items
        //playerDets.softCurrency = InventorySystem.Get().GetAmount(InventorySystem.k_coinsId); //Grab coins
        //playerDets.hardCurrency = InventorySystem.Get().GetAmount(InventorySystem.k_gemsId); //Grab gems

        StartCoroutine(SaveAll());

    }

    IEnumerator SaveAll()
    {
        InventorySystem.Set().SetAmount(InventorySystem.k_coinsId, playerDets.softCurrency); //Save coins

        yield return new WaitForSeconds(0.1f);

        InventorySystem.Set().SetAmount(InventorySystem.k_gemsId, playerDets.hardCurrency); //Save gems
        yield return new WaitForSeconds(0.1f);

        InventorySystem.Set().SetAmount(InventorySystem.k_scoreId, playerDets.score); //Save gems
        yield return new WaitForSeconds(0.1f);

        InventorySystem.Set().SetAmount(InventorySystem.k_tutorial, playerDets.tutorial); //Save gems
        yield return new WaitForSeconds(0.1f);

        InventorySystem.Set().SetAmount(InventorySystem.k_currentCat, playerStab.currentCat); //Save current cat
        yield return new WaitForSeconds(0.1f);

        InventorySystem.Set().SetAmount(InventorySystem.k_currentRoom, playerStab.currentRoom); //Save current cat
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < maxCats; i++)
        {

            InventorySystem.Set().SetAmount(InventorySystem.k_CatNum[i], playerInv.catsUnlocked[i]); //Save unlocked cats
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < maxSkin; i++)
        {

            InventorySystem.Set().SetAmount(InventorySystem.k_CatSkins[i], playerInv.catSkins[i]);  //Save cat skins
            yield return new WaitForSeconds(0.1f);

        }
        
        for (int i = 0; i < maxHead; i++)
        {

            InventorySystem.Set().SetAmount(InventorySystem.k_HeadNum[i], playerInv.catHeadItems[i]); //Save cat head items
            Debug.Log(InventorySystem.k_HeadNum[i]);
            yield return new WaitForSeconds(0.1f);
        }
        

        for (int i = 0; i < maxCollar; i++)
        {
            InventorySystem.Set().SetAmount(InventorySystem.k_CollarNum[i], playerInv.catCollarItems[i]); //Save collar items
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < maxRoom; i++)
        {
            InventorySystem.Set().SetAmount(InventorySystem.k_RoomNum[i], playerInv.catRoomItems[i]); //Save collar items
            yield return new WaitForSeconds(0.1f);
        }

        //----------------------------
        //After retrieving all of the players current items, we save their cat setups

        for (int i = 0; i < maxCats; i++)
        {
            InventorySystem.Set().SetAmount(InventorySystem.k_CatSkinCurr[i], playerStab.catSkinCurrent[i]);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < maxCats; i++)
        {
            InventorySystem.Set().SetAmount(InventorySystem.k_CatHeadCurr[i], playerStab.catHeadCurrent[i]);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < maxCats; i++)
        {
            InventorySystem.Set().SetAmount(InventorySystem.k_CatCollarCurr[i], playerStab.catCollarCurrent[i]);
            yield return new WaitForSeconds(0.1f);
        }

        //playerInv.catsUnlocked = InventorySystem.Get().GetAmount(InventorySystem.k_CatNum); //Grab gems
    }
}
