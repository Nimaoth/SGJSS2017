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
    public Text multiplierText;
    private float currentScore;
    private float multiplier = 1.0f;
    private float lastReset;
    private bool dead = false;

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
        if (!dead)
        {
            if (tag == "right")
                sc.text = (int)currentScore + " :Score\nx" + multiplier;
            else
                sc.text = "Score: " + (int)currentScore;

            multiplierText.text = "x " + multiplier.ToString("0.00");

            currentScore += multiplier * 10 * Time.deltaTime;

            if (Time.time - lastReset > 5)
            {
                multiplier += 0.1f * Time.deltaTime;
            }

        }
    }

    public void resetMultiplier()
    {
        multiplier = 1;
        lastReset = Time.time;
    }

    public void addScore(string tag)
    {
        switch (tag)
        {
            case "SpeedUp":
                currentScore += multiplier * 55;
                break;

            case "SlowDown":
                currentScore = Mathf.Clamp(currentScore - 35, 0, float.MaxValue);
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
                currentScore = Mathf.Clamp(currentScore - 1000, 0, float.MaxValue); ;
                dead = true;
                break;
        }
    }
}
