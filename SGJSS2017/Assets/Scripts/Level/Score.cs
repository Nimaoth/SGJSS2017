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
    private float multiplier = 1.0f;

	// Use this for initialization
	void Start()
    {
        if (tag == "right")
            sc.text = "0 " + sc.text;
        else
            sc.text += "0" ;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (tag == "right")
            sc.text = (int)currentScore + " :Score";
        else
            sc.text = "Score: " + (int)currentScore;

        currentScore += multiplier * 10 * Time.deltaTime;


    }

    public void addScore(string tag)
    {
        switch (tag)
        {
            case "SpeedUp":
                currentScore += multiplier * 55;
                break;

            case "SlowDown":
                currentScore -= 35;
                break;

            case "Mine":
                currentScore += multiplier * 300;
                break;

            case "Seastar":
                currentScore += multiplier * 200;
                break;

            case "Rock":
                currentScore += multiplier * 100;
                break;

            case "Wall":
                currentScore += multiplier * 100;
                break;

            case "Death":
                currentScore -= 1000;
                break;
        }
    }
}
