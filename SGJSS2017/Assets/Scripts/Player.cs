using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int MAX_HEALTH = 3;

    public GameObject Shockwave;

    public int ID;
    public int lives = 3;
    public float playerSpeed;

    private Score score;
    public Score Score
    {
        get
        {
            return score;
        }
    }

    private Rigidbody playerRigid;

    private Transform ModelTransform;

    // sound
    private AudioClip fishSound;

    //for Shot cooldown
    public float FIRE_COOLDOWN;
    private float fireTimerCounter;

    // ui
    private Lives[] LiveBar;

    // ParticleSystem for buffs
    public GameObject PickUpHealthParticleSystem;
    public GameObject SpeedBuffParticleSystem;
    public GameObject SpeedDebuffParticleSystem;

    private bool hasSpeedBuff = false;
    private float speedBuff = 1.0f;
    private float speedBuffEndTime = 0.0f;
    private GameObject speedBuffPS;

    // last pusher
    private Player lastPusher;
    private float lastPushTime;

    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        ID = Game.Instance.getPlayerID(this);
        playerRigid = GetComponent<Rigidbody>();
        ModelTransform = transform.Find("Model");

        GameObject UI = GameObject.FindWithTag("PlayerUI");
        Transform scorePlayer = UI.transform.GetChild(ID - 1);

        LiveBar = new Lives[scorePlayer.childCount - 1];
        for (int i = 0; i < LiveBar.Length; i++)
            LiveBar[i] = scorePlayer.GetChild(i).GetComponent<Lives>();

        score = scorePlayer.GetComponentInChildren<Score>();

        fishSound = Game.Instance.getPlayerHitSound(ID);
    }

    // Update is called once per frame
    void Update()
    {

        float speed = playerSpeed;
        if (hasSpeedBuff)
        {
            if (Time.time >= speedBuffEndTime)
            {
                // cancel
                StopSpeedBuff();
            }
            else
            {
                speed *= speedBuff;
            }
        }

        #region Player Movement
        //player Movement
        Vector3 force = new Vector3();
        force.x = Input.GetAxis("Horizontal" + ID) * speed * Time.deltaTime;
        force.y = Input.GetAxis("Vertical" + ID) * speed * Time.deltaTime;
        playerRigid.AddForce(force * 150, ForceMode.Acceleration);

        // dont move outside of screen
        Rect bounds = Game.Instance.Bounds;
        if (transform.position.y > bounds.yMax)
            playerRigid.AddForce(new Vector3(0, -0.5f * playerRigid.velocity.magnitude, 0), ForceMode.Impulse);

        if (transform.position.y < bounds.yMin)
            playerRigid.AddForce(new Vector3(0, 0.5f * playerRigid.velocity.magnitude, 0), ForceMode.Impulse);

        if (transform.position.x > bounds.xMax)
            playerRigid.AddForce(new Vector3(-0.5f * playerRigid.velocity.magnitude, 0, 0), ForceMode.Impulse);

        if (transform.position.x < bounds.xMin)
            playerRigid.AddForce(new Vector3(0.5f * playerRigid.velocity.magnitude, 0, 0), ForceMode.Impulse);

        // direction
        Vector3 dir = new Vector3(0, -Game.Instance.Speed, 0) * 1 + playerRigid.velocity;
        if (dir.magnitude > 0.3f)
            ModelTransform.rotation = Quaternion.LookRotation(dir, new Vector3(0, 0, 1));

        #endregion

        #region Shockwave
        //Spieler kann sich mit der fire1 (viereck) taste aufblasen und eine Shockwave instantiaten, die nahe Spieler wegstoeßt
        if (Input.GetButtonDown("Fire" + ID))
        {
            if (fireTimerCounter <= 0)
            {
                GameObject xyz = Instantiate(Shockwave, transform.position, Quaternion.LookRotation(dir, Vector3.forward));
                xyz.GetComponent<Shockwave>().creator = this;
                fireTimerCounter = FIRE_COOLDOWN;


                Game.PlayClip(fishSound, 1);

                StartCoroutine(Blow());
            }
        }


        if (fireTimerCounter >= 0)
        {
            fireTimerCounter -= Time.deltaTime;
        }

        #endregion

        var v = transform.position;
        v.z = 0;
        transform.position = v;
    }

    IEnumerator Blow()
    {
        float t = Time.time;

        float s = 0;
        while (Time.time - t < 0.3f)
        {
            s = (Time.time - t) * 3 * 10f / 3f * Mathf.PI;

            rend.material.SetFloat("_Schlider", Mathf.Sin(s) + 1);
            yield return new WaitForEndOfFrame();
        }
        rend.material.SetFloat("_Schlider", 0);

    }

    public void Push(Vector3 force, bool damage = false, Player player = null)
    {
        lastPusher = player;
        lastPushTime = Time.time;
        playerRigid.AddForce(force, ForceMode.Impulse);
        if (damage)
            TakeDamage();
    }

    public Player getLastPusher()
    {
        if (Time.time - lastPushTime <= 1)
        {
            return lastPusher;
        }
        return null;
    }

    public void TakeDamage()
    {
        if (lives > 0)
        {
            LiveBar[lives - 1].show(false);
        }

        lives--;



        if (lives <= 0)
        {
            score.addScore("Death");
            lives = 0;
            // TODO game over
            Destroy(gameObject);

            Game.Instance.playerDied();
        }
    }

    public void Heal()
    {
        if (lives <= 2)
        {
            LiveBar[lives].show(true);
        }

        lives++;
        if (lives > MAX_HEALTH)
            lives = MAX_HEALTH;
    }

    public void SpeedBuff(float duration, float amount)
    {
        if (!hasSpeedBuff)
        {
            hasSpeedBuff = true;

            if (amount > 1)
            {
                speedBuffPS = Instantiate(SpeedBuffParticleSystem, transform.position, Quaternion.identity, transform);
            }
            else
            {
                speedBuffPS = Instantiate(SpeedDebuffParticleSystem, transform.position, Quaternion.identity, transform);
            }

            speedBuffEndTime = Time.time + duration;
        }
        else
        {
            if ((amount >= 1 && speedBuff >= 1) || (amount < 1 && speedBuff < 1))
            {
                // same buff, so refresh timer
                speedBuffEndTime = Time.time + duration;
            }
            else
            {
                // cancel
                StopSpeedBuff();
            }
        }
        speedBuff = amount;
    }

    public void StopSpeedBuff()
    {
        speedBuffPS.GetComponent<ParticleSystem>().Stop();
        Destroy(speedBuffPS, 2);
        hasSpeedBuff = false;
    }

    private IEnumerator SpeedBuffCoroutine(float time, float amount)
    {
        hasSpeedBuff = true;

        playerSpeed *= amount;
        GameObject ps;
        if (amount > 1)
        {
            ps = Instantiate(SpeedBuffParticleSystem, transform.position, Quaternion.identity, transform);
        }
        else
        {
            ps = Instantiate(SpeedDebuffParticleSystem, transform.position, Quaternion.identity, transform);
        }

        yield return new WaitForSeconds(time);

        ParticleSystem p = ps.GetComponent<ParticleSystem>();
        p.Stop();

        Destroy(ps, 2);
        playerSpeed /= amount;

        hasSpeedBuff = false;
        yield return null;
    }

}
