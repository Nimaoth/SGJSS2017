using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {

    private Rigidbody playerRigid;

    public float playerSpeed;
    public Rect Bounds;

    private Transform modelTransform;

    // Use this for initialization
    void Start () {
        modelTransform = transform.Find("Model");
        playerRigid = GetComponent<Rigidbody>();

        BubbleManager.Instance.Events.Add(() => new BubbleManager.BubbleForce(transform.position, 10 + 5 * playerRigid.velocity.magnitude));    
    }

    // Update is called once per frame
    void Update () {
        float speed = playerSpeed;

        //player Movement
        Vector3 force = new Vector3();
        force.x = Input.GetAxis("Horizontal1") * speed * Time.deltaTime;
        force.y = Input.GetAxis("Vertical1") * speed * Time.deltaTime;
        playerRigid.AddForce(force * 150, ForceMode.Acceleration);

        // dont move outside of screen
        if (transform.position.y > Bounds.yMax)
            playerRigid.AddForce(new Vector3(0, -0.5f * playerRigid.velocity.magnitude, 0), ForceMode.Impulse);

        if (transform.position.y < Bounds.yMin)
            playerRigid.AddForce(new Vector3(0, 0.5f * playerRigid.velocity.magnitude, 0), ForceMode.Impulse);

        if (transform.position.x > Bounds.xMax)
            playerRigid.AddForce(new Vector3(-0.5f * playerRigid.velocity.magnitude, 0, 0), ForceMode.Impulse);

        if (transform.position.x < Bounds.xMin)
            playerRigid.AddForce(new Vector3(0.5f * playerRigid.velocity.magnitude, 0, 0), ForceMode.Impulse);

        // direction
        Vector3 dir = playerRigid.velocity;
        if (dir.magnitude > 0.3f)
            modelTransform.rotation = Quaternion.LookRotation(dir, new Vector3(0, 0, 1));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(Bounds.center.x, Bounds.center.y, 0), new Vector3(Bounds.width, Bounds.height, 1));
    }
}
