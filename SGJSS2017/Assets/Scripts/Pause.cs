﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pause : MonoBehaviour
{
    bool paused = false;

    void Update()
    {
        Debug.Log("test");
        if (Input.GetButtonDown("pauseButton"))
            paused = togglePause();
        
    }

    void OnGUI()
    {
       
        if (paused)
        {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Click me to unpause"))
                paused = togglePause();
        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}

