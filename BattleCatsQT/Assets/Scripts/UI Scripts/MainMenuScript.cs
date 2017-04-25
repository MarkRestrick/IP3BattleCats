using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject gameData;
    PlayerDetailsScript playerDets;
    public GameObject playPanel;
    public GameObject quitPanel;
    public GameObject friendsPanel;
    public GameObject difficultyPanel;
    public GameObject eventPanel;
    public GameObject tutorialPanel;
    public GameObject tutorialTap;
    public GameObject tutorialHold;
    public GameObject tutorialSlider;
    public GameObject customizationPanel;
    public GameObject bottomMenuPanel;
    public GameObject friendRoomMenuPanel;
    Animator customizationAnimator;
    Animator bottomMenuAnimator;
    Animator friendsRoomMenuAnimator;
    public bool playMenuActive = false;
    public bool customizationActive = false;
    public bool bottomMenuActive = true;
    public bool friendsMenuActive = false;
    public bool friendRoomActive = false;
    public bool friendsRoomMenuActive = false;
    public GameObject loadingScreenPanel;
    public GameObject menuCamera;
    public GameObject friendCamera;
    MenuCameraScript menuCameraScript;
    GameOptionsScript gameOptionsScript; // Needed for returning to LevelSelection instead of directly to main menu
    GameObject gameDataObject;  // Needed for returning to LevelSelection instead of directly to main menu 


    /*void Start() // CURRENTLY Needed for returning to LevelSelection instead of directly to main menu 
    {
        gameDataObject = GameObject.FindGameObjectWithTag("GameData"); //JS Grab the object that contains the data
        gameOptionsScript = gameDataObject.GetComponent<GameOptionsScript>(); //Grab player data

        if (gameOptionsScript.returningFromGamePlay) // if loaded from gameplay goes directly to event selection
        {
            ActivatePlayPanel();
            gameOptionsScript.returningFromGamePlay = false; // resets returning from game play
        }
    }
    */

    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData");
        playerDets = gameData.GetComponent<PlayerDetailsScript>();

        ActivateBottomMenu();
    }

    public void ActivatePlayPanel()
    {
        if (!playPanel.activeSelf) // Activate play panel
        {
            playPanel.SetActive(true);
            difficultyPanel.SetActive(true);
            eventPanel.SetActive(false);
            ActivateBottomMenu();
        }
        else if (playPanel.activeSelf && difficultyPanel.activeSelf) // Close play panel
        {
            playPanel.SetActive(false);
            difficultyPanel.SetActive(true);
            eventPanel.SetActive(false);
            ActivateBottomMenu();
        }
        else if (playPanel.activeSelf && eventPanel.activeSelf) // Close Event and go back to difficulty
        {
            difficultyPanel.SetActive(true);
            eventPanel.SetActive(false);
        }
    }

    // This is now Side Menu, but I didn't change the name for this. Animator Bool/Trigger is new.
    public void ActivateBottomMenu()
    {
        bottomMenuAnimator = bottomMenuPanel.GetComponent<Animator>();

        if (!bottomMenuActive) //old one: customizationPanel.activeSelf
        {
            bottomMenuActive = true;
            bottomMenuAnimator.SetBool("sideMenuActive", true);
            bottomMenuAnimator.SetTrigger("sideMenuToggle");
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.CameraZoomToggle();
            
        }

        else if (bottomMenuActive)
        {
            bottomMenuActive = false;
            bottomMenuAnimator.SetBool("sideMenuActive", false);
            bottomMenuAnimator.SetTrigger("sideMenuToggle");
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.CameraZoomToggle();
            
        }
    }



    public void ActivateEventSelection()
    {
        difficultyPanel.SetActive(false);
        eventPanel.SetActive(true);
    }


    public void ActivateCustomization()
    {
        customizationAnimator = customizationPanel.GetComponent<Animator>();
        
        if (!customizationActive) //old one: customizationPanel.activeSelf
        {
            customizationActive = true;            
            Debug.Log("Customization Activated");
            customizationPanel.SetActive(true);
            customizationAnimator.SetBool("customizationActive", true);
            customizationAnimator.SetTrigger("customizationToggle");            
            ActivateBottomMenu();
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.CameraZoomToggle();
        }

        else if (customizationActive)
        {
            customizationActive = false;
            customizationAnimator.SetBool("customizationActive", false);
            customizationAnimator.SetTrigger("customizationToggle");
            StartCoroutine(AnimationCustomizationPause(0.5f));

            
            ActivateBottomMenu();
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.CameraZoomToggle();
        }
    }

    IEnumerator AnimationCustomizationPause(float sec)
    {
        //customizationAnimator = customizationPanel.GetComponent<Animator>();
        //customizationAnimator.SetBool("customizationActive", false);
        //customizationAnimator.SetTrigger("customizationToggle");
        yield return new WaitForSeconds(sec);
        customizationPanel.SetActive(false);
        Debug.Log("Customization Dectivated");
    }

    public void LoadLevel()
    {
        //Application.LoadLevel("MainMenu");
        StartCoroutine(LoadAsync());

        // Activate Loading screen
        loadingScreenPanel.SetActive(true);
    }

    IEnumerator LoadAsync()
    {
        if(playerDets.tutorial == 1)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync("Gameplay");
            yield return async;
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
        
        
        Debug.Log("Level loaded");
    }


    public void ActivateQuitPanel()
    {
        customizationAnimator = customizationPanel.GetComponent<Animator>();

        if (customizationActive)
        {
            ActivateCustomization();
            //customizationAnimator.SetBool("customizationActive", false);
            //customizationPanel.SetActive(false);
            //menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            //menuCameraScript.CameraZoomToggle();
        }

        else if (friendsMenuActive) //IF Friend selection is active, toggle it off
        {
            ActivateFriendsPanel();
        }

        else if (friendRoomActive) //IF Friend room is active, toggle it off
        {
            ActivateFriendRoom();
        }

        else if (playPanel.activeSelf) //IF Friend selection is active, toggle it off
        {
            ActivatePlayPanel();
        }

        else if (!quitPanel.activeSelf) // Activate quit panel
        {
            quitPanel.SetActive(true);
        }
        else if (quitPanel.activeSelf) // Close quit panel
        {
            quitPanel.SetActive(false);
        }
    }



    public void ActivateTutorial()
    {
        if (!tutorialPanel.activeSelf) // Activate tutorial & tap panel
        {
            tutorialPanel.SetActive(true);
            tutorialTap.SetActive(true);
            tutorialHold.SetActive(false);
            tutorialSlider.SetActive(false);
        }

        else if (tutorialTap.activeSelf) // Activate hold panel
        {
            tutorialTap.SetActive(false);
            tutorialHold.SetActive(true);
            tutorialSlider.SetActive(false);
        }

        else if (tutorialHold.activeSelf) // Activate slider panel
        {
            tutorialTap.SetActive(false);
            tutorialHold.SetActive(false);
            tutorialSlider.SetActive(true);
        }

        else if (tutorialSlider.activeSelf) // Go to game
        {
            //Application.LoadLevel("Gameplay");
            SceneManager.LoadScene("Gameplay");
        }

    }

    /* public void LoadFriendsScene()
     {
         //Application.LoadLevel("Friends");
         SceneManager.LoadScene("Friends");
     }
     */

    public void ActivateFriendsPanel() // ACTIVATES MENU FOR SELECTION OF FRIENDS
    {
        if (!friendsMenuActive && friendsRoomMenuActive) // Returning from Friend room
        {
            friendsPanel.SetActive(true);
            friendsMenuActive = true;
            friendsRoomMenuActive = false;
        }
        else if (!friendsMenuActive) // Activate quit panel
        {
            friendsPanel.SetActive(true);
            friendsMenuActive = true;
            ActivateBottomMenu();
        }
        else if (friendsMenuActive) // Close quit panel
        {
            friendsPanel.SetActive(false);
            friendsMenuActive = false;
            ActivateBottomMenu();
        }
    }


    public void ActivateFriendRoom() // TOGGLES CAMERA FROM PLAYER TO FRIEND ROOM
    {
        friendsRoomMenuAnimator = friendRoomMenuPanel.GetComponent<Animator>();

        if (!friendRoomActive)
        {
            menuCamera.SetActive(false);
            friendCamera.SetActive(true);
            friendRoomActive = true;

            friendsPanel.SetActive(false); // DEACTIVATE FRIEND CHOOSING PANEL
            friendsMenuActive = false;

            //ActivateFriendRoomUI();
            friendsRoomMenuActive = true;
            friendRoomMenuPanel.SetActive(true);
            friendsRoomMenuAnimator.SetBool("bottomMenuActive", true);
            friendsRoomMenuAnimator.SetTrigger("bottomMenuToggle"); 
            //ActivateBottomMenu();
        }

        else if (friendRoomActive)
        {
            menuCamera.SetActive(true);
            friendCamera.SetActive(false);
            friendRoomActive = false;

            //friendRoomMenuPanel.SetActive(false);
                        
            friendsRoomMenuAnimator.SetBool("bottomMenuActive", false); //SOMETHING WRONG HERE
            friendsRoomMenuAnimator.SetTrigger("bottomMenuToggle"); //SOMETHING WRONG HERE
            ActivateFriendsPanel();
        }
    }




    public void LoadCustomizeScene()
    {
        //Application.LoadLevel("Customize");
        SceneManager.LoadScene("Customize");
    }

    public void LoadMainMenuScene()
    {
        //Application.LoadLevel("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }


    public void ApplicationQuit()
    {
        Application.Quit();
    }


}
