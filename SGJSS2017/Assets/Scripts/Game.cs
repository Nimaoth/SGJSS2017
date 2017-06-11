using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    private static Game s_instance;

    public static Game Instance
    {
        get
        {
            return s_instance;
        }
    }

    public float MaxSpeed;
    public float Speed;
    public float SpeedIncrease;

    public AudioClip[] PlayerHitSounds;

    public float AmbientDecrease;


    private int nextPlayerID;
    private float deathCounter;

    private void Awake()
    {
        s_instance = this;
        nextPlayerID = 1;
    }


    private void Update()
    {
        Speed += SpeedIncrease * Time.deltaTime;
        if (Speed > MaxSpeed)
            Speed = MaxSpeed;


        Color amb = RenderSettings.ambientLight - new Color(AmbientDecrease, AmbientDecrease, AmbientDecrease) * Speed * Time.deltaTime;

        float min = 0.2f;
        if (amb.r < min)
            amb.r = min;
        if (amb.g < min)
            amb.g = min;
        if (amb.b < 1.1f * min)
            amb.b = 1.1f * min;

        RenderSettings.ambientLight = amb;
    }

    public void LoadScene(string scene, float time = 0.0f)
    {
        StartCoroutine(LoadSceneCo(scene, time));
    }

    public IEnumerator LoadSceneCo(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

    public int getPlayerID()
    {
        return nextPlayerID++;
    }

    public void playerDied()
    {
        deathCounter++;
        if (deathCounter == nextPlayerID - 2)
            SceneManager.LoadScene("EndScene");
    }

    public AudioClip getPlayerHitSound(int id)
    {
        return PlayerHitSounds[id - 1];
    }
}
