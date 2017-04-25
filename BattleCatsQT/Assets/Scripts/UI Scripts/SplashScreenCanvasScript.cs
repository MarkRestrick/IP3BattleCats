using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenCanvasScript : MonoBehaviour {

    public GameObject loadingScreenPanel;
    public PlayGuestButtonScript playScript;

    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        playScript.GuestDetails();
        LoadMainMenuScene();
    }

    public void LoadMainMenuScene()
    {


        // Activate Loading screen
        loadingScreenPanel.SetActive(true);


        //Application.LoadLevel("MainMenu");
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation async = SceneManager.LoadSceneAsync("MainMenu");
        yield return async;
        Debug.Log("Level loaded");
    }


}
