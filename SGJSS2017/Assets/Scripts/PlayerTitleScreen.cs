using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTitleScreen : MonoBehaviour {
    public float playerSpeed;
    //public float yAxisWrap = 4;
   // public int ID;

    //private Rigidbody playerRigid;

    // Use this for initialization
    void Start () {
       // ID = Game.Instance.getPlayerID();
        //playerRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
       // Vector3 force = new Vector3();
        float amtToMove = Input.GetAxisRaw("Horizontal1") * playerSpeed * Time.deltaTime;
        float amtToMoveUp = Input.GetAxisRaw("Vertical1") * playerSpeed * Time.deltaTime;



        transform.Translate(amtToMove * Vector3.right, Space.World);
        transform.Translate(amtToMoveUp * Vector3.up, Space.World);
    }
}
