using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float AmbientDecrease;

    private void Start()
    {
        s_instance = this;
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
}
