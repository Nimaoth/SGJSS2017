using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour {

    private Vector3 vel;
    private Vector3 acc;
    private Vector3 target;
    private Vector3 desired;
    private Vector3 steering;
    private float maxSpeed = 15;
    private float maxForce = 25;

    public GameObject bubbleExplosion;
    public Renderer r;

    public LayerMask mask;

    // Use this for initialization
    void Start()
    {

        //Set level
        RenderSettings.ambientLight = new Color(1, 1, 1);

        //give every bubble a random size
        float random = Random.Range(0.2f, 0.4f);
        transform.localScale = new Vector3(random, random, random);
        target = new Vector3(transform.position.x, transform.position.y);

        vel = new Vector3();
        vel = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1);
    }
    	
	// Update is called once per frame
	void Update () {

        //Vector3 direction = new Vector3(Random.Range(1, 10), Random.Range(1, 10), transform.position.z);
        //transform.Translate(direction * Time.deltaTime);

        //seek direction
        desired = target - transform.position;
        desired *= 1;
        
        if (desired.magnitude > maxSpeed)
        {
            desired.Normalize();
            desired *= maxSpeed;
        }

        steering = desired - vel;
        steering *= 2;
        if (steering.magnitude > maxForce)
        {
            steering.Normalize();
            steering *= maxForce;
        }

        acc += steering;

        foreach (var bf in BubbleManager.Instance.Forces)
        {
            var pos = bf.Location;
            pos.z = transform.position.z;

            Vector3 dir = transform.position - pos;
            float dist = dir.sqrMagnitude;

            float strength = bf.Strength / dist;
            if (strength > maxForce)
                strength = maxForce;
            acc += dir.normalized * strength;

        }


        vel += acc * Time.deltaTime;
        transform.position += vel * Time.deltaTime;
        acc *= 0;

        if (Input.anyKeyDown)
        {
            StartCoroutine(pop());
        }

	}

    

    IEnumerator pop()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2.5f));

        GameObject.Instantiate(bubbleExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());
        r.enabled = false;
        Destroy(this);
    }
}
