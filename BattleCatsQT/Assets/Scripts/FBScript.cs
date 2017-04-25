using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FBScript : MonoBehaviour {

    public GameObject userDetailsObject;
    PlayerDetailsScript playerDetails;
    public Text playerNameText;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }

    void Start()
    {
        userDetailsObject = GameObject.FindGameObjectWithTag("GameData");
        playerDetails = userDetailsObject.GetComponent<PlayerDetailsScript>();
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }

    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
        
        
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }

            GrabDetails(FB.IsLoggedIn);
        }
    }

    void GrabDetails(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
        }
    }

    void DisplayUsername(IResult result)
    {
        //Text UserName = username.GetComponent<Text>();

        if (result.Error == null)
        {
            playerDetails.userName = result.ResultDictionary["first_name"] as string;
            playerDetails.userID = AccessToken.CurrentAccessToken.UserId;
            playerNameText.text = "Hold on a sec, " + result.ResultDictionary["first_name"] as string;
            SceneManager.LoadSceneAsync("MainMenu");

        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}
