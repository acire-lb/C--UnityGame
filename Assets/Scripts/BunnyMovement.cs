using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyMovement : MonoBehaviour {

    private Rigidbody2D myRigidBody2d;
    private Animator MyAnim; 
    public float bunnyjumpforce = 500f;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

	// Use this for initialization
	void Start () {
        myRigidBody2d = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
	}

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
        }

        if (bunnyHurtTime == -1)
        {
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)
            {
                if (myRigidBody2d.velocity.y < 0)
                {
                    myRigidBody2d.velocity = Vector2.zero;
                }
                if (jumpsLeft == 1)
                {
                    myRigidBody2d.AddForce(transform.up * bunnyjumpforce * 0.75f);
                }
                else
                {
                    myRigidBody2d.AddForce(transform.up * bunnyjumpforce);
                }
                
                jumpsLeft--;
                jumpSfx.Play();
            }
            MyAnim.SetFloat("vVlosity", myRigidBody2d.velocity.y);
            //set parameter to current velocity, controlling the physics in the y direction.
            //on every frame update we are goign to on the animator set the v vilocity parameter to to the current velocity of the bunny is either falling down or jumping up.
            scoreText.text = (Time.time - startTime).ToString("0.0");

            //   MyAnim.SetFloat("vVlosity", Mathf.Abs(myRigidBody2d.velocity.y));
            // maths.abs takes that absolute value of the focring this to always be a positive value, 
        }
        else
        {
            //
            if (Time.time > bunnyHurtTime + 2)// if current time is greater then bunny hurt time 2 seconds layer, we load level.
            {

                Application.LoadLevel(Application.loadedLevel);
            }
            }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        //when somehting collides with bunny
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enimy"))
        {
            foreach (PrefabSpawnObject spawner in FindObjectsOfType<PrefabSpawnObject>())
            {
                spawner.enabled = false;
                //disable objects with pre fab spawner.
                //stops cactus form cloning when bunny hits previous cactus.
            }
            foreach (MoveLeft movelefter in FindObjectsOfType<MoveLeft>())
            {//find in current scene objects with move left component
                //loop through the list - grab each object that has been returned.
                movelefter.enabled = false;

            }
            bunnyHurtTime = Time.time;//current point in time
            MyAnim.SetBool("BunnyHurt", true);//trigger animiation
            myRigidBody2d.velocity = Vector2.zero;
            myRigidBody2d.AddForce(transform.up * bunnyjumpforce);
            myCollider.enabled = false;

            deathSfx.Play();

            float currentBestScore = PlayerPrefs.GetFloat("BestScore", 0);
            float currentScore = Time.time - startTime;// calculate score just acheived

            if (currentScore > currentBestScore)
            {
                PlayerPrefs.SetFloat("BestScore", currentScore);//save as new best score.
            }

        }

        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2; 

        }
    }
}
