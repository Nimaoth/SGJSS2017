using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

    public Vector3 RotationAxis;
    public float RotationSpeed;

    void Update()
    {
        float offset = Game.Instance.Speed * Time.deltaTime;
        transform.Translate(0, offset, 0);

        // destroy if out of screen
        if (transform.position.y > 50)
            Destroy(gameObject);

        transform.Rotate(RotationAxis, RotationSpeed * Time.deltaTime);
    }
}
