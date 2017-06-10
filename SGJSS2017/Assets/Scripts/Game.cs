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

    public float Speed;
    public float SpeedIncrease;

    private void Start()
    {
        s_instance = this;
    }

    private void Update()
    {
        Speed += SpeedIncrease * Time.deltaTime;
    }
}
