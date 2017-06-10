using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Shockwave;

    public int maxHealth = 3;
    public int lives = 3;
    public float playerSpeed;
    public float yAxisWrap = 4;
    private Vector3 velocity;
    private Rigidbody playerRigid;
    public string horizontal, vertical, fire;

    private Transform ModelTransform;

    //for Shot cooldown
    public float FIRE_COOLDOWN;
    private float fireTimerCounter;

    // ParticleSystem for buffs
    public GameObject PickUpHealthParticleSystem;
    public GameObject SpeedBuffParticleSystem;

    private bool hasSpeedBuff = false;

    // Use this for initialization
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        ModelTransform = transform.Find("Model");
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Movement
        //player Movement
        Vector3 force = new Vector3();
        force.x = Input.GetAxis(horizontal) * playerSpeed * Time.deltaTime;
        force.y = Input.GetAxis(vertical) * playerSpeed * Time.deltaTime;
        playerRigid.AddForce(force * 150, ForceMode.Acceleration);

        // dont move outside of screen
        if (transform.position.y > yAxisWrap)
            Push(new Vector3(0, -0.5f * playerRigid.velocity.magnitude, 0));
        //playerRigid.velocity = new Vector3(playerRigid.velocity.x, -playerRigid.velocity.y, playerRigid.velocity.z);

        if (transform.position.y < -yAxisWrap)
            Push(new Vector3(0, 0.5f * playerRigid.velocity.magnitude, 0));

        // direction
        Vector3 dir = new Vector3(0, -Game.Instance.Speed, 0) * 1 + playerRigid.velocity;
        if (dir.magnitude > 0.3f)
            ModelTransform.rotation = Quaternion.LookRotation(dir, new Vector3(0, 0, 1));

        #endregion

        #region Shockwave
        //Spieler kann sich mit der fire1 (viereck) taste aufblasen und eine Shockwave instantiaten, die nahe Spieler wegstoeßt
        if (Input.GetButtonDown(fire))
        {
            if (fireTimerCounter <= 0)
            {
                GameObject xyz = Instantiate(Shockwave, transform.position, Quaternion.LookRotation(dir, Vector3.forward));
                if (tag == "player1")
                {
                    xyz.GetComponent<Shockwave>().tagSet = "player2";
                }
                else if (tag == "player2")
                    xyz.GetComponent<Shockwave>().tagSet = "player1";
                fireTimerCounter = FIRE_COOLDOWN;
            }
        }

        if (fireTimerCounter >= 0)
        {
            fireTimerCounter -= Time.deltaTime;
        }

        #endregion

        if (lives <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.J))
            SpeedBuff(3, 2);
    }

    public void Push(Vector3 force, int damage = 0)
    {
        playerRigid.AddForce(force, ForceMode.Impulse);
        lives -= damage;
    }

    public void Heal(int amount)
    {
        lives += amount;
        if (lives > maxHealth)
            lives = maxHealth;
    }

    public void SpeedBuff(float time, float amount)
    {
        if (!hasSpeedBuff)
            StartCoroutine(SpeedBuffCoroutine(time, amount));
    }

    private IEnumerator SpeedBuffCoroutine(float time, float amount)
    {
        hasSpeedBuff = true;

        playerSpeed *= amount;
        GameObject ps = Instantiate(SpeedBuffParticleSystem, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(time);

        ParticleSystem p = ps.GetComponent<ParticleSystem>();
        p.Stop();

        Destroy(ps, 2);
        playerSpeed /= amount;

        hasSpeedBuff = false;
        yield return null;
    }
}
