using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    /*Man bekommt Punkte, wenn:
     * - Wenn der Gekickte in einem bestimmten Zeitraum schaden bekommt (1 Sekunde, 100 Punkte: gegen Wand, 200: gegen Seestern, 300: gegen Bombe)
     * - Wenn man PowerUps einsammelt (Speed: 55 Punkte, Slow: - 35 Punkte)
     * - 10 Punkte pro Sekunde ->  1.5x Multiplier alle 5 Sekunden, die man nicht mit Hindernissen kollidiert
     * - -1000 Punkte Abzug wenn man stirbt
     */
    public Text sc;
    private float currentScore;

	// Use this for initialization
	void Start () {
        if (tag == "right")
            sc.text = "0 " + sc.text;
        else
            sc.text += "0" ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
