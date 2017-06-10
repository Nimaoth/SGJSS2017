using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> Obstacles;

    public Transform ObstaclesParent;

    public float MaxSpawnRate;
    public float SpawnRate;
    public float SpawnRageChange;
    public float LevelWidth;

    private float lastSpawn = 0;

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Random.value < 0.001 * SpawnRate * (Time.time - lastSpawn))
        {
            lastSpawn = Time.time;

            // spawn obstacles here
            SpawnObstacle();
        }

        SpawnRate += SpawnRageChange * Time.deltaTime;
        if (SpawnRate > MaxSpawnRate)
            SpawnRate = MaxSpawnRate;
	}

    private void SpawnObstacle()
    {
        // select a random obstacle to spawn
        int index = Random.Range(0, Obstacles.Count);

        // create the object
        GameObject prefab = Obstacles[index];
        Vector3 pos = ObstaclesParent.transform.position;
        pos.x += Random.Range(-LevelWidth, LevelWidth);
        GameObject instance = Instantiate(prefab, pos, Quaternion.identity, ObstaclesParent);

    }

    private void OnDrawGizmosSelected()
    {
        Vector3 size = new Vector3(2 * LevelWidth, 1, 1);
        Gizmos.DrawWireCube(transform.position, size);
    }
}
