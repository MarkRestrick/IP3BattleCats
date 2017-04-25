using UnityEngine;
using System.Collections;

public class TestSaveScript : MonoBehaviour {
    public InventoryUIController InvControl;
    public GameObject data;

	// Use this for initialization
	void Start ()
    {
        data = GameObject.FindGameObjectWithTag("GameData");
        InvControl = data.GetComponent<InventoryUIController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        InvControl.OnInventorySave();
    }
}
