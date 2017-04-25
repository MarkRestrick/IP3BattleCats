using System;
using System.Collections.Generic;
using UnityEngine;
using ChilliConnect;

/// Simple key value store that holds the local copy of the ChilliConnect inventory. This is updated
/// locally and on the server whenever an IAP purchase is made
/// 
public class InventorySystem
{
    private static InventorySystem s_singletonInstance = null;

    public event System.Action OnInventoryUpdated = delegate { };
    public const string k_coinsId = "COINS";
    public const string k_gemsId = "GEMS";
    public const string k_scoreId = "SCORE";
    public const string k_currentCat = "CURRENTCAT";
    public const string k_currentRoom = "CURRENTROOM";
    public const string k_tutorial = "TUTORIAL";

    public static readonly string[] k_CatNum = { "CATTYPE0", "CATTYPE1", "CATTYPE2", "CATTYPE3", "CATTYPE4", "CATTYPE5" };
    public static readonly string[] k_CatSkins = { "CATSKIN0", "CATSKIN1", "CATSKIN2", "CATSKIN3", "CATSKIN4", "CATSKIN5", "CATSKIN6", "CATSKIN7" };
    public static readonly string[] k_HeadNum = { "CATHEAD0", "CATHEAD1", "CATHEAD2", "CATHEAD3", "CATHEAD4" };
    public static readonly string[] k_CollarNum = { "CATCOLLAR0", "CATCOLLAR1", "CATCOLLAR2", "CATCOLLAR3", "CATCOLLAR4" };
    public static readonly string[] k_RoomNum = { "ROOM1", "ROOM2", "ROOM3" };

    public static readonly string[] k_CatSkinCurr = { "CAT1SKINEQUIP", "CAT2SKINEQUIP", "CAT3SKINEQUIP", "CAT4SKINEQUIP", "CAT5SKINEQUIP", "CAT6SKINEQUIP" };
    public static readonly string[] k_CatHeadCurr = { "CAT1HEADEQUIP", "CAT2HEADEQUIP", "CAT3HEADEQUIP", "CAT4HEADEQUIP", "CAT5HEADEQUIP", "CAT6HEADEQUIP" };
    public static readonly string[] k_CatCollarCurr = { "CAT1COLLAREQUIP", "CAT2COLLAREQUIP", "CAT3COLLAREQUIP", "CAT4COLLAREQUIP", "CAT5COLLAREQUIP", "CAT6COLLAREQUIP" };

    public const string k_Skin0 = "SKIN0";

    ChilliConnectSdk chilli;


    private Dictionary<string, int> m_inventory = new Dictionary<string, int>();

    /// @return Singleton instance if system has been created (not lazily created)
    /// 
    public static InventorySystem Get()
    {
        return s_singletonInstance;
    }

    public static InventorySystem Set()
    {
        return s_singletonInstance;
    }


    /// 
    public InventorySystem()
    {
        s_singletonInstance = this;
    }

    /// Fetch the inventory contents from ChilliConnect at the start of the game.
    /// 
    /// @param chilliConnect
    /// 	SDK instance
    /// 
    public void Initialise(ChilliConnectSdk chilliConnect)
    {
        Debug.Log("Fetching inventory");
        chilli = chilliConnect;
        chilliConnect.Economy.GetCurrencyBalance(new GetCurrencyBalanceRequestDesc(), OnCurrencyBalanceFetched, (request, error) => Debug.LogError(error.ErrorDescription));
    }

    /// Called when the inventory currency balance is pulled from ChilliConnect. Builds a local
    /// copy of the inventory that we can keep up to date
    /// 
    /// @param request
    /// 	Request
    /// @param response
    /// 	Contents of the inventory
    /// 
    private void OnCurrencyBalanceFetched(GetCurrencyBalanceRequest request, GetCurrencyBalanceResponse response)
    {
        
        //Debug.Log("Inventory fetched: ");
        foreach (var item in response.Balances)
        {
            AddItem(item.Key, item.Balance);
        }
        
    }

    /// Add an amount of the given item to inventory. Whenever an item is added
    /// a notification is fired allowing the inventory UI to update.
    /// 
    /// @param itemId
    /// 	Item to add
    /// @param amount
    /// 	Amount to add
    /// 
    public void AddItem(string itemId, int amount)
    {
        
        //Debug.Log(string.Format("Adding {0} of item {1} to local inventory", amount, itemId));

        int currentAmount = 0;
        if (m_inventory.TryGetValue(itemId, out currentAmount) == true)
        {
            m_inventory[itemId] = currentAmount + amount;
        }
        else
        {
            m_inventory.Add(itemId, amount);
        }
        
        OnInventoryUpdated();
    }

    /// @param itemId
    /// 	Id of the inventory item
    /// @return Amount of given item in inventory
    /// 
    public int GetAmount(string itemId)
    {
        int amount = 0;
        m_inventory.TryGetValue(itemId, out amount);
        return amount;
    }

    public void SetAmount(string itemID, int amount)
    {
        //chilliConnect.Economy.GetCurrencyBalance(new GetCurrencyBalanceRequestDesc(), OnCurrencyBalanceFetched, (request, error) => Debug.LogError(error.ErrorDescription));
        chilli.Economy.SetCurrencyBalance(new SetCurrencyBalanceRequestDesc(itemID, amount), Woot, (request, error) => Debug.LogError(error.ErrorDescription));

        
    }

    private void Woot(SetCurrencyBalanceRequest request, SetCurrencyBalanceResponse response)
    {
        
    }
}
