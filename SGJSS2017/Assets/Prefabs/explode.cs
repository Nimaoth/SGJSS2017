using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {

    public GameObject silvesterPink;
    public GameObject silvesterGreen;

	// Use this for initialization
	void Start () {

        Instantiate(silvesterPink, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
