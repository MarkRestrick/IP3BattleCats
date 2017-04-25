using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InGameButtonScript : MonoBehaviour {

    public GameObject gameMenuPanel;
    public GameObject sideMenuPanel;
    Animator sideMenuAnimator;
    public bool sideMenuActive = false;


    GameOptionsScript gameOptionsScript;
    GameObject gameDataObject;



    //Toggle Side Menu
    public void ActivateSideMenu()
    {
        sideMenuAnimator = sideMenuPanel.GetComponent<Animator>();

        if (!sideMenuActive) //old one: customizationPanel.activeSelf
        {
            sideMenuActive = true;
            sideMenuAnimator.SetBool("sideMenuActive", true);
            sideMenuAnimator.SetTrigger("sideMenuToggle");
            StartCoroutine(Pause());
        }

        else if (sideMenuActive)
        {
            Time.timeScale = 1;
            sideMenuActive = false;
            sideMenuAnimator.SetBool("sideMenuActive", false);
            sideMenuAnimator.SetTrigger("sideMenuToggle");
            
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.5f);
        if(!sideMenuActive)
        {
            Time.timeScale = 1;
        }
        else if (sideMenuActive)
        {
            Time.timeScale = 0;
        }
    }

    // Reload TestLevel
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit Application
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        gameDataObject = GameObject.FindGameObjectWithTag("GameData"); //JS Grab the object that contains the data
        gameOptionsScript = gameDataObject.GetComponent<GameOptionsScript>(); //JS Grab options data to carry info that player is returning for gameplay
        gameOptionsScript.returningFromGamePlay = true;
        SceneManager.LoadScene(1);
    }


    // Toggle Game menu buttons
    public void ActivateGameMenu()
    {
        if (!gameMenuPanel.activeSelf)
        {
            gameMenuPanel.SetActive(true);
            ActivateSideMenu();
        }

        else
        {
            //gameMenuPanel.SetActive(false);
            ActivateSideMenu();
        }
    }
}
