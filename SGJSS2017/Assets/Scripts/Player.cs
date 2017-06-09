using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float playerSpeed;
    public static int lives = 3;
    public float xAxisWrap = 10;
    private Vector3 velocity;
    private Rigidbody playerRigid;
    public string horizontal, vertical;


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
    }
}
