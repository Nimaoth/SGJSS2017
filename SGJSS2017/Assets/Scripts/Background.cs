using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    
    public float SpeedMultiplier;

	// Update is called once per frame
	void Update () {
        transform.Translate(0, Game.Instance.Speed * SpeedMultiplier * Time.deltaTime, 0);
	}
}
