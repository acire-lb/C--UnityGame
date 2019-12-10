using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

    public float speed = 5;//can tweek in inspector.



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.left * speed * Time.deltaTime;
        // taking the current position of the  cactus and add to it a left vector 
        //delta time is the time it took to complete the last frame.
	}
}
