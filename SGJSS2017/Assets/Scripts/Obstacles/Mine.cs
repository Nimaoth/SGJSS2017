using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public float PushStrength;
    public GameObject ExplosionPrefab;

    private Material material;


    // Use this for initialization
    void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update()
    {
        float offset = Game.Instance.Speed * Time.deltaTime;
        transform.Translate(0, offset, 0);

        // destroy if out of screen
        if (transform.position.y > 50)
            Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Explode(other.transform);
        }
    }

    private void Explode(Transform player)
    {
        PushPlayer(player);

        // spawn explosion
        if (ExplosionPrefab != null)
        {
            Debug.Log("explosde");
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        // destroy this game object
        Destroy(gameObject);
    }

    private void PushPlayer(Transform player)
    {
        // direction from mine to player
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 force = direction * PushStrength;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
    }
}
