using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour {

    private bool boom;

	// Use this for initialization
	void Start () {
        boom = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroy()
    {
        boom = true;
        StartCoroutine(blink());
    }

    public void add (float count)
    {
        StartCoroutine(blink());
    }

    IEnumerator blink ()
    {
        yield return new WaitForSeconds(1.0f);

        //set oppacity lower
        Color c = this.GetComponent<Renderer>().material.color;

        if (boom && c.a >= 0f)
            c.a -= 0.1f;

        if (!boom && c.a <= 1.0f)
            c.a += 0.1f;
    }
}
