using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public GameObject clickPrefab;
    public GameObject slidePrefab;
    public GameObject holdPrefab;
    public GameObject playerDataObject;
    public GameObject tutClick;
    public GameObject tutSlide;
    public GameObject tutHold;
    public PlayerDataScript playerData;
    public int goodHits;
    public float clickHitDelay;
    public float holdDelay;
    public float slideDelay;
    public bool ClickTrigger = false;
    public bool SlideTrigger = false;
    public bool HoldTrigger = false;
    public Animator tutorialFeedback;
    public ObjectActivateScript objectScript;
    public Button theButton;

    public GameObject GameData;
    public PlayerDetailsScript playerDets;

	// Use this for initialization
	void Start ()
    {
        GameData = GameObject.FindGameObjectWithTag("GameData");
        playerDets = GameData.GetComponent<PlayerDetailsScript>();
        playerDets.tutorial = 1;
        StartCoroutine(GoGoTutorial());
        
	}

    IEnumerator GoGoTutorial()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1f);
        ShowHit();
    }
	
	// Update is called once per frame
	void Update ()
    {
        goodHits = playerData.successfulHits;

        if (ClickTrigger == true)
        {
           
            PracticeHit();
        }

        if(SlideTrigger == true)
        {
            PracticeSlide();
        }

        if(HoldTrigger == true)
        {
            PracticeHold();
        }
    }

    void ShowHit()
    {
        GameObject clickInstance = Instantiate(tutClick, transform.position, transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        objectScript = clickInstance.GetComponent<ObjectActivateScript>();
        theButton = objectScript.linkedItem.GetComponent<Button>();
        theButton.interactable = false;
        StartCoroutine(EnableButton(theButton));
        ClickTrigger = true;


    }

    IEnumerator EnableButton(Button theButton)
    {
        yield return new WaitForSeconds(2f);
        theButton.interactable = true;
    }

    void PracticeHit()
    {
        
        if(gameObject.transform.childCount == 0)
        {
            tutorialFeedback.SetTrigger("Good");
            StartCoroutine(HitTime());
            ClickTrigger = false;
        }
                
        
    }

    IEnumerator HitTime()
    {
        yield return new WaitForSeconds(clickHitDelay);
        playerData.successfulHits = 0;
        GameObject clickInstance = Instantiate(clickPrefab, transform.position, transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(2.0f);
        clickInstance = Instantiate(clickPrefab, transform.position + new Vector3(50, 0, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(2.0f);
        clickInstance = Instantiate(clickPrefab, transform.position + new Vector3(-50, 0, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(2.0f);
        clickInstance = Instantiate(clickPrefab, transform.position + new Vector3(0, 20, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(5.0f);
        EvaluateHits();
    }

    void EvaluateHits()
    {
        if(goodHits > 2)
        {
            tutorialFeedback.SetTrigger("Good");
            ShowSlide();
        }
        else
        {
            tutorialFeedback.SetTrigger("TryAgain");
            StartCoroutine(HitTime());
        }
    }

    void ShowSlide()
    {
        GameObject clickInstance = Instantiate(tutSlide, transform.position + new Vector3(-50,0,0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        SlideTrigger = true;
    }

    void PracticeSlide()
    {
        
        if (gameObject.transform.childCount == 0)
        {
            tutorialFeedback.SetTrigger("Good");
            StartCoroutine(SlideTime());
            SlideTrigger = false;
        }
    }

    IEnumerator SlideTime()
    {
        yield return new WaitForSeconds(slideDelay);
        playerData.successfulHits = 0;
        GameObject clickInstance = Instantiate(slidePrefab, transform.position + new Vector3(-50, 0, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(4f);
        clickInstance = Instantiate(slidePrefab, transform.position + new Vector3(50, 0, 0), Quaternion.Euler(new Vector3(0,0,180))) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(4f);
        clickInstance = Instantiate(slidePrefab, transform.position + new Vector3(-50, -30, 0), Quaternion.Euler(new Vector3(0, 0, 45))) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(5f);
        EvaluateSlides();
    }

    void EvaluateSlides()
    {
        if (goodHits > 1)
        {
            tutorialFeedback.SetTrigger("Good");
            ShowHold();
        }
        else
        {
            tutorialFeedback.SetTrigger("TryAgain");
            StartCoroutine(SlideTime());
        }
    }

    void ShowHold()
    {
        GameObject clickInstance = Instantiate(tutHold, transform.position, transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        objectScript = clickInstance.GetComponent<ObjectActivateScript>();
        theButton = objectScript.linkedItem.GetComponent<Button>();
        theButton.interactable = false;
        StartCoroutine(EnableButton(theButton));
        HoldTrigger = true;
    }

    void PracticeHold()
    {
        if (gameObject.transform.childCount == 0)
        {
            tutorialFeedback.SetTrigger("Good");
            StartCoroutine(HoldTime());
            HoldTrigger = false;
        }
    }

    IEnumerator HoldTime()
    {
        yield return new WaitForSeconds(holdDelay);
        playerData.successfulHits = 0;
        GameObject clickInstance = Instantiate(holdPrefab, transform.position, transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(3.5f);
        clickInstance = Instantiate(holdPrefab, transform.position + new Vector3(50, 0, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(3.5f);
        clickInstance = Instantiate(holdPrefab, transform.position + new Vector3(-50, 0, 0), transform.rotation) as GameObject;
        clickInstance.transform.SetParent(gameObject.transform);
        clickInstance.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        yield return new WaitForSeconds(5.0f);
        EvaluateHolds();
    }

    void EvaluateHolds()
    {
        if (goodHits > 1)
        {
            tutorialFeedback.SetTrigger("Good");
            FinishIt();
        }
        else
        {
            tutorialFeedback.SetTrigger("TryAgain");
            StartCoroutine(SlideTime());
        }
    }

    void FinishIt()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }
}
