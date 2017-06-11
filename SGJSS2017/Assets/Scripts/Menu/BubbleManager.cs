using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {

    public struct BubbleForce
    {
        public Vector3 Location;
        public float Strength;

        public BubbleForce(Vector3 loc, float str)
        {
            Location = loc;
            Strength = str;
        }
    }

    private static BubbleManager _instance;

    public static BubbleManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<BubbleForce> Forces;

    public delegate BubbleForce GetForceLocation();
    public List<GetForceLocation> Events;

    private void Awake()
    {
        _instance = this;
        Events = new List<GetForceLocation>();
        Forces = new List<BubbleForce>();
    }

    private void Update()
    {
        Forces.Clear();
        for (int i = 0; i < Events.Count; i++)
        {
            Forces.Add(Events[i]());
        }
    }
}
