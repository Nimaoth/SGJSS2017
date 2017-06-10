using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

    public List<Transform> Backgrounds;
    public float SpeedMultiplier;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, Game.Instance.Speed * SpeedMultiplier * Time.deltaTime, 0);


        foreach (var bg in Backgrounds)
        {
            if (bg.position.y > 75)
                bg.Translate(0, -200, 0);
        }		
	}
}
