﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    
    public GameObject ExplosionPrefab;
    public float RotationSpeed;
   
     
     

    // Use this for initialization
    void Start()
    {
	    
	}
	

    private void OnTriggerEnter(Collider other)
    {
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
