using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SceneManager.LoadScene("ViolaScene");
        }
    }
}
