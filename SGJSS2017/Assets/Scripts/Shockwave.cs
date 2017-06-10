    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float PushStrength;
    public int creator;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player p = other.transform.parent.GetComponent<Player>();
            if (p.ID != creator)
                PushPlayer(p);
        }
    }

    private void PushPlayer(Player player)
    {
        // direction from mine to player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 force = direction * PushStrength;

        player.Push(force);
    }
}