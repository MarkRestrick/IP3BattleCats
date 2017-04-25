using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFXButtonScript : MonoBehaviour {

    public GameObject screenFXPanel;
    public Text fxButtonText;

    void Start()
    {
        screenFXPanel.SetActive(false);
        fxButtonText.text = "Screen FX OFF";
    }

    public void screenFXToggle()
    {
        if (screenFXPanel.activeSelf) // Deactivate play panel
        {
            screenFXPanel.SetActive(false);
            fxButtonText.text = "Screen FX OFF";
        }

        else if (!screenFXPanel.activeSelf) // Activate play panel
        {
            screenFXPanel.SetActive(true);
            fxButtonText.text = "Screen FX ON";
        }
    }
}
