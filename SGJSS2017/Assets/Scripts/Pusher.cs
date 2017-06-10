using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MovableObject
{
    public float PushStrength;
    public bool DestroyOnHit;
    public bool Damage;
    public GameObject ExplosionPrefab;
    public AudioClip Clip;
    public float Volume = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.parent.GetComponent<Player>();
            Explode(player);
        }
    }

    private void Explode(Player player)
    {
        PushPlayer(player);

        // spawn explosion
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        if (Clip != null)
        {
            AudioSource.PlayClipAtPoint(Clip, transform.position, Volume);
        }

        // destroy this game object
        if (DestroyOnHit)
            Destroy(gameObject);
    }

    private void PushPlayer(Player player)
    {
        // direction from mine to player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 force = direction * PushStrength;

        player.Push(force, Damage);
    }

}
