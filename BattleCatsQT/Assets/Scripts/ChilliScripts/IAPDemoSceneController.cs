using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using ChilliConnect;
using SdkCore;

/// The main controller class for the IAP demo. Creates the systems and handles the main control flow
/// and intialises ChilliConnect.
/// 
/// IAP Demo: 
/// - Shows a simple purchase catalogue populated by items specified via ChilliConnect dashboard
/// - Allows the user to purchase and redeem items which will update the ChilliConnect and local inventory
/// - Demo works for Apple and GooglePlay (but Kindle is also supported).
/// 
/// More info on setting up ChilliConnect can be found here: https://docs.chilliconnect.com/guide/setup/
/// 
public class IAPDemoSceneController : MonoBehaviour
{
    const string GAME_TOKEN = "6WPoMFRzKJrCApXtbikiYwGGeDwD27mF";

    String[] friendsList = new string[] { "Null", "Null", "Null", "Null", "Null" };
    public PlayerDetailsScript PlayerDetails;

    private ChilliConnectSdk m_chilliConnect = null;
    private IAPSystem m_iapSystem = new IAPSystem();
    private InventorySystem m_inventorySystem = new InventorySystem();
    public bool userCheck = false;

    /// Initialised ChilliConnect, create and log in a player.
    /// 
    private void Awake()
    {


        // Initialise ChilliConnect. Game token can be found on the game dashboard of ChilliConnect
        m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN, true);

        // Create a new ChilliConnect player with the given display name if we don't have any credentials saved for the local player
        if (PlayerPrefs.HasKey("CCId") == true && PlayerPrefs.HasKey("CCSecret") == true)
        {
            Debug.Log("Player already exists. Logging in");
            m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(PlayerPrefs.GetString("CCId"), PlayerPrefs.GetString("CCSecret"), (loginRequest) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
            Debug.Log(PlayerPrefs.GetString("CCId"));
        }
        else
        {
            Debug.Log("Creating Player");
            var requestDesc = new CreatePlayerRequestDesc();
            requestDesc.DisplayName = "TestyMcTestface";
            m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, OnPlayerCreated, (AddEventRequest, error) => Debug.LogError(error.ErrorDescription));

            
        }
    }

    /// Called when player creation has completed allowing us to log the
    /// player in
    /// 
    /// @param request
    /// 	Info on request made to create player
    /// @param response
    /// 	Holds the id and secret to log the player in
    /// 
    private void OnPlayerCreated(CreatePlayerRequest request, CreatePlayerResponse response)
    {
        Debug.Log("Player created. Logging in");

        //Save the credentials so we don't create a new player next time we launch the app
        PlayerPrefs.SetString("CCId", response.ChilliConnectId);
        PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);
        PlayerPrefs.Save();
        m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(response.ChilliConnectId, response.ChilliConnectSecret, (loginRequest) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
    }

    /// Called on successful login to ChilliConnect and initialises the systems we
    /// require for this demo. Now jump to CatalogueController.cs to see the purchase flow or
    /// Inventory system to see how the inventory is obtained.
    /// 
    private void OnLoggedIn()
    {
        Debug.Log("Login successful. Initialising IAP and Inventory");

        //Now we have logged in, we can pull down the player inventory and the catalogue
        m_iapSystem.Initialise(m_chilliConnect);
        m_inventorySystem.Initialise(m_chilliConnect);
        //SetUser();
        GetFriends();
    }

    public void SetUser(string username)
    {
        var userDesc = new SetPlayerDetailsRequestDesc();
        userDesc.UserName = username;
        m_chilliConnect.PlayerAccounts.SetPlayerDetails(userDesc, successCallbackUser, errorCallbackUser);
    }



    Action<SetPlayerDetailsRequest, SetPlayerDetailsResponse> successCallbackUser = (SetPlayerDetailsRequest request, SetPlayerDetailsResponse response) =>
    {
        
    };

    Action<SetPlayerDetailsRequest, SetPlayerDetailsError> errorCallbackUser = (SetPlayerDetailsRequest request, SetPlayerDetailsError response) =>
    {

    };


    Action<LookupUserNamesRequest, LookupUserNamesResponse> successCallback = (LookupUserNamesRequest request, LookupUserNamesResponse response) =>
    {
        if (response.Players.Count == 0)
        {
            UnityEngine.Debug.Log("No player found for the usernames provided.");
        }
        else
        {
            foreach (var player in response.Players)
            {
                UnityEngine.Debug.Log("We found it");
            }
        }
    };

    Action<LookupUserNamesRequest, LookupUserNamesError> errorCallback = (LookupUserNamesRequest request, LookupUserNamesError error) =>
    {
        UnityEngine.Debug.Log("An error occurred while getting player data: " + error.ErrorDescription);
    };
    
    public void AddFriend()
    {
        m_chilliConnect.PlayerAccounts.LookupUserNames(new List<string>() { "MarkyMark" }, successCallback, errorCallback);


        var friendsList2 = new MultiTypeListBuilder().Add(friendsList[0]).Add(friendsList[1]).Add(friendsList[2]).Add(friendsList[3]).Add(friendsList[4]).Build();
        
        
        var cloudData = m_chilliConnect.CloudData;

        Action<SetPlayerDataRequest, SetPlayerDataResponse> successCallbackFriend = (SetPlayerDataRequest request, SetPlayerDataResponse response) =>
        {
            UnityEngine.Debug.Log("Player data set successfully! Many ponies!");
        };

        Action<SetPlayerDataRequest, SetPlayerDataError> errorCallbackFriend = (SetPlayerDataRequest request, SetPlayerDataError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while setting player data: " + error.ErrorDescription);
        };

        var requestDescFriend = new SetPlayerDataRequestDesc("FriendsList", friendsList2);
        cloudData.SetPlayerData(requestDescFriend, successCallbackFriend, errorCallbackFriend);
    }

    //////////////////////////////////////
    
    public void GetFriends()
    {
        var cloudData = m_chilliConnect.CloudData;

        Action<GetPlayerDataRequest, GetPlayerDataResponse> successCallback = (GetPlayerDataRequest request, GetPlayerDataResponse response) =>
        {
            if (response.Values.Count == 0)
            {
                UnityEngine.Debug.Log("No data for the requested cloud player data key.");
                return;
            }

            var playerData = response.Values[0];
            var characterData = playerData.Value.AsList();
            var scores = characterData.GetString(0);
            Debug.Log("We passed the list");
            
            /*
            foreach (var score in characterData)
            {
                Debug.Log("Friend:    " + score);
                Debug.Log(scores);
            }
            */
            
            for(int i = 0; i < characterData.Count; i++)
            {
                friendsList[i] = characterData.GetString(i);
                Debug.Log(friendsList[i]);
            }
        };

        Action<GetPlayerDataRequest, GetPlayerDataError> errorCallback = (GetPlayerDataRequest request, GetPlayerDataError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while getting player data: " + error.ErrorDescription);
        };

        var keys = new List<string>() { "FriendsList" };
        cloudData.GetPlayerData(keys, successCallback, errorCallback);

        PlayerDetails.userName = friendsList[0];
    }

    
}
