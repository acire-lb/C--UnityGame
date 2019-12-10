using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnObject : MonoBehaviour {
    //private PrefabSpawnObject 

    private float nextSpawn = 0; // hold next point in time when want to spawn a cactus
    public Transform prefabToSpawm;// hold reference to prefab - where we will assign it to cactus.
   // public float spawnRate = 1;//time in seconds between spawing our prefabs
   // public float randomDelay = 1;// spawn rate plus random delay.
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;
    private float startTime;
    public float jitter = 0.25f;
    


	// Use this for initialization
	void Start () {
        startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
        // if it has passed the next spawn time.
        if(Time.time > nextSpawn)
        {
            // pass prefab wish to spaw, position (catus spaw object), rotation 
            Instantiate(prefabToSpawm, transform.position, Quaternion.identity);
            //nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);

            float curvePos = (Time.time - startTime) / curveLengthInSeconds;
            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;

            }
            nextSpawn = Time.time + spawnCurve.Evaluate(curvePos) + Random.Range(-jitter, jitter);
        }
		
	}
}
