using UnityEngine;
using System.Collections;

public class RoomScript : MonoBehaviour {

    public GameObject catDataObject;
    PlayerStableScript playerStable;

    public GameObject[] Rooms;


    // Use this for initialization
    void Start () {

        catDataObject = GameObject.FindGameObjectWithTag("GameData");
        playerStable = catDataObject.GetComponent<PlayerStableScript>();
        SpawnRoom();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnRoom()
    {
        for(int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i].SetActive(false);
        }
        Rooms[playerStable.currentRoom - 1].SetActive(true);
    }
}
