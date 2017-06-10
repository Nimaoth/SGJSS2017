using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float PushStrength;
    private string tag;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setTag()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            Explode(other.transform);
        }

        Destroy(gameObject);
    }

    private void Explode(Transform player)
    {
        PushPlayer(player);

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