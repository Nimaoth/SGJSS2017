using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseText;

    bool paused = false;

    void Update()
    {
        if (Input.GetButtonDown("pause"))
            paused = togglePause();
        
    }

    bool togglePause()
    {
        if (Time.timeScale < 0.5f)
        {
            // resume
            PauseText.SetActive(false);
            Time.timeScale = 1f;
            return false;
        }
        else
        {
            // pause
            PauseText.SetActive(true);
            Time.timeScale = 0f;
            return true;
        }
    }
}

