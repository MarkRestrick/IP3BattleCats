using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelIntroScript : MonoBehaviour {

    public GameObject introCamera;
    public Camera catCamera;
    public GameObject levelLight;
    public int introLength;
    public int countdownLength = 4;
    public GameObject countdownPanel;
    public GameObject statsPanel;
    public GameObject worldUICanvas;
    public GameObject screenVignette;
    private Light lt;
    public float lightDuration = 1.0f;
    public float lightStartAmplitude = 1.5f;
    public float lightDimAmplitude = 0.4f;

    // Use this for initialization
    void Start () {
        introCamera.SetActive(true);
        catCamera.enabled = false;
        statsPanel.SetActive(true);
        IntroStart();

        lt = levelLight.GetComponent<Light>();
        lt.intensity = lightStartAmplitude;
    }
	

    public void IntroStart()
    {
        StartCoroutine(IntroFinishTimer());
    }

    IEnumerator IntroFinishTimer()
    {
        yield return new WaitForSeconds(introLength);

        lt = levelLight.GetComponent<Light>();
        
        introCamera.SetActive(false);
        catCamera.enabled = true;
        countdownPanel.SetActive(true);
        statsPanel.SetActive(false);
        worldUICanvas.SetActive(false);
        screenVignette.SetActive(true);

        //float phi = Time.time / lightDuration * 2 * Mathf.PI;
        //float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
        lt.intensity = lightDimAmplitude;
        CountdownDisable();

    }

    public void CountdownDisable()
    {
        StartCoroutine(DisableCountdownPanelTimer());
    }


    IEnumerator DisableCountdownPanelTimer()
    {
        yield return new WaitForSeconds(countdownLength);

        countdownPanel.SetActive(false);

    }


}
