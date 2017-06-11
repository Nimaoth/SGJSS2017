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
        Highscore.text = "";

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
