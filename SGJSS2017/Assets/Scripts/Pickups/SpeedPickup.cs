﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MovableObject
{

    public float Duration;
    public float Strength;
    public AudioClip Clip;
    public float Volume = 1.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player p = other.transform.parent.GetComponent<Player>();
            Buff(p);
            Destroy(gameObject);

            p.Score.addScore(tag);

            if (Clip != null)
            {
                Game.PlayClip(Clip, Volume);
            }
        }
    }

    private void Buff(Player player)
    {
        player.SpeedBuff(Duration, Strength);
    }
}
