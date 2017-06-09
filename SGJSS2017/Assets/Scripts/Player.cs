using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float playerSpeed;
    public static int lives = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //player Movement
        #region PlayerMovement
        //Move player based on user input
        float amtToMove = Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;
        float amtToMoveUp = Input.GetAxisRaw("Vertical") * playerSpeed * Time.deltaTime;



        #endregion
    }
}
