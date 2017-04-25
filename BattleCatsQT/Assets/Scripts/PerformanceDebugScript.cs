using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PerformanceDebugScript : MonoBehaviour {

    public Text fps;
    public GameObject catSpawner;
    public CatSpawnScript catSpawnScript;
    public GameObject catInstance;

    Vector3 spawnLoc = new Vector3(-10f, -0.2f, -2f);
    float updateRate = 4.0f;
    float deltaTime = 0.0f;
    float fpsNum = 0.0f;
    float frameCounter = 0.0f;

	
	// Update is called once per frame
	void Update ()
    {
        frameCounter++;
        deltaTime += Time.deltaTime;
        if(deltaTime > 1.0/updateRate)
        {
            fpsNum = Mathf.Round(frameCounter / deltaTime);
            fps.text = fpsNum.ToString();
            frameCounter = 0;
            deltaTime -= 1.0f / updateRate;
        }
	
	}

    public void MoveAndSpawn()
    {
        catInstance = Instantiate(catSpawner, spawnLoc, Quaternion.identity) as GameObject;
        spawnLoc += new Vector3(0.0f, 0.0f, 0.2f);
        catSpawnScript.SpawnOwnCat();
    }
}
