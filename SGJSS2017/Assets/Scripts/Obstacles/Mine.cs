using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public float PushStrength;
    public GameObject ExplosionPrefab;
    public float RotationSpeed;
    public bool DestroyOnHit;
    public int Damage;

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

        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "player1" || other.tag == "player2")
        {
            Explode(other.transform.parent);
        }
    }

    private void Explode(Transform player)
    {
        PushPlayer(player);

        // spawn explosion
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        // destroy this game object
        if (DestroyOnHit)
            Destroy(gameObject);
    }

    private void PushPlayer(Transform player)
    {
        // direction from mine to player
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 force = direction * PushStrength;

        player.GetComponent<Player>().Push(force, Damage);
    }
}
