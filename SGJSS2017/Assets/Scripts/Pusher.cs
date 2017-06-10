using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MovableObject
{
    public float PushStrength;
    public bool DestroyOnHit;
    public bool Damage;
    public GameObject ExplosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
