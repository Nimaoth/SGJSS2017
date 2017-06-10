using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject Shockwave;

    public float playerSpeed;
    public static int lives = 3;
    public float xAxisWrap = 10;
    private Vector3 velocity;
    private Rigidbody playerRigid;
    public string horizontal, vertical;

    //for Shot cooldown
    public float FIRE_COOLDOWN;
    private float fireTimerCounter;

    // Use this for initialization
    void Start () {
        playerRigid = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        //player Movement
        #region PlayerMovement
        //Move player based on user input
        float amtToMove = Input.GetAxis(horizontal) * playerSpeed * Time.deltaTime;
        float amtToMoveUp = Input.GetAxis(vertical) * playerSpeed * Time.deltaTime;


        //transform.Translate(amtToMove * Vector3.right, Space.World);
        //transform.Translate(amtToMoveUp * Vector3.up, Space.World);

        Vector3 force = new Vector3();
        force.x = amtToMove;
        force.y = amtToMoveUp;
        playerRigid.AddForce(force*100, ForceMode.Acceleration);

        //Screen wrap x axis
        if (transform.position.x < -xAxisWrap)
        {
            transform.position = new Vector3(-xAxisWrap, transform.position.x, transform.position.z);
        }

        if (transform.position.x > xAxisWrap)
        {
            transform.position = new Vector3(xAxisWrap, transform.position.x, transform.position.z);
        }

        // transform.Translate(velocity * Time.deltaTime);
        // velocity = velocity * 0.5f;
        #endregion

        #region Shockwave
        //Spieler kann sich mit der fire1 (viereck) taste aufblasen und eine Shockwave instantiaten, die nahe Spieler wegstoeßt
        if (Input.GetButtonDown("Fire1"))
        {
            if (fireTimerCounter <= 0)
            {
                GameObject xyz = Instantiate(Shockwave, transform.position, Quaternion.identity);
                if(tag == "player1")
                {
                    xyz.GetComponent<Shockwave>().tagSet = "player2";
                }
                else if(tag == "player2")
                    xyz.GetComponent<Shockwave>().tagSet = "player1";
                fireTimerCounter = FIRE_COOLDOWN;
            }
        }

        if (fireTimerCounter >= 0)
        {
            fireTimerCounter -= Time.deltaTime;
        }

        #endregion
    }
}
