using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public float Duration;
    public float Strength;

    public enum Stat
    {
        Health,
        Speed
    }

    public Stat stat;

	// Update is called once per frame
	void Update () {
        float offset = Game.Instance.Speed * Time.deltaTime;
        transform.Translate(0, offset, 0);

        // destroy if out of screen
        if (transform.position.y > 50)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player1" || other.tag == "player2")
        {
            Buff(other.transform.parent);
        }
    }

    private void Buff(Transform player)
    {
        Player p = player.GetComponent<Player>();

        switch (stat)
        {
            case Stat.Health:
                p.Heal((int)Strength);
                break;

            case Stat.Speed:
                p.SpeedBuff(Duration, Strength);
                break;
        }
    }
}
