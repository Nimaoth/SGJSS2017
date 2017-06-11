using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<Player> list;
    public static List<KeyValuePair<int, float>> highscores;

    private void Awake()
    {
        s_instance = this;
        nextPlayerID = 1;
        list = new List<Player>();
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

    public int getPlayerID(Player player)
    {
        list.Add(player);
        return nextPlayerID++;     
    }

    public void playerDied()
    {
        deathCounter++;
        if (deathCounter == nextPlayerID - 2)
        {


            highscores = list.Select(p => new KeyValuePair<int, float>(p.ID, p.score.getScore())).ToList();
            highscores.Sort((p1, p2) => (int)(p2.Value - p1.Value));
            SceneManager.LoadScene("Highscore");
        }
            
    }

    public AudioClip getPlayerHitSound(int id)
    {
        return PlayerHitSounds[id - 1];
    }
}
