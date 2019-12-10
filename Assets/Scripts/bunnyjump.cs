using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyjump : MonoBehaviour {

    private Rigidbody2D myRigidBody2d;
    private Animator MyAnim; 
    public float bunnyjumpforce = 500f; 

	// Use this for initialization
	void Start () {
        myRigidBody2d = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Jump"))
        {
            myRigidBody2d.AddForce(transform.up * bunnyjumpforce);
        }
        MyAnim.SetFloat("vVlosity", myRigidBody2d.velocity.y);
        //set parameter to current velocity, controlling the physics in the y direction.
        //on every frame update we are goign to on the animator set the v vilocity parameter to to the current velocity of the bunny is either falling down or jumping up.


        //   MyAnim.SetFloat("vVlosity", Mathf.Abs(myRigidBody2d.velocity.y));
        // maths.abs takes that absolute value of the focring this to always be a positive value, 
    }
   void OnCollisionEnter2D(Collision2D collision)
    {
        
        //when somehting collides with bunny
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enimy"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
           
            

        
    }
}
