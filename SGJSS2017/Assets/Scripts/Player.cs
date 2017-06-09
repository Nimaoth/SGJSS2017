using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float playerSpeed;
    public static int lives = 3;
    public float xAxisWrap = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //player Movement
        #region PlayerMovement
        //Move player based on user input
        float amtToMove = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float amtToMoveUp = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.Translate(amtToMove * Vector3.right, Space.World);
        transform.Translate(amtToMoveUp * Vector3.up, Space.World);

        //Screen wrap x axis
        if (transform.position.x < -xAxisWrap)
        {
            transform.position = new Vector3(-xAxisWrap, transform.position.x, transform.position.z);
        }

        if (transform.position.x > xAxisWrap)
        {
            transform.position = new Vector3(xAxisWrap, transform.position.x, transform.position.z);
        }
        #endregion
    }
}
