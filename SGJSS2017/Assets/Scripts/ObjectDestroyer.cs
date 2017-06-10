using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    public float Time;

	// Use this for initialization
	void Start()
    {
        Destroy(gameObject, Time);
	}
}
