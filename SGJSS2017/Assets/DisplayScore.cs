using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

    int player;
    int value;
    public Text Highscore;

	// Use this for initialization
	void Start () {

        Color red = new Color(207, 0, 10);
        Color blue = new Color(22, 41, 85);
        Color green = new Color(53, 111, 19);
        Color yellow = new Color(255, 174, 11);
        

        for (int i = 0; i < Game.highscores.Count; i++)
        {
            player = Game.highscores[i].Key;
            value = (int) Game.highscores[i].Value;

            Highscore.text += "Player " + player + ": " + value + "\n" + "\n";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
