using System;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject model;
    public LayerMask whatIsGround;
    public float spawnRange;
    public float averageSpawnTime;

    public float minDistance;
    public float maxDistance;

    DateTimeOffset nextSpawn;

    int spawns = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = DateTimeOffset.Now.AddSeconds(UnityEngine.Random.Range(-10, 10) + averageSpawnTime);
        Debug.Log($"NextSpawn in : {(nextSpawn - DateTimeOffset.Now).TotalSeconds}");
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTimeOffset.Now > nextSpawn)
        {
            spawns++;
            for (int i = 0; i < spawns; i++)
            {
                var spawnTransform = spawnLocation.transform;
                var spawnBasePosition = spawnTransform.position;
                float randomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

                float randomDirection = UnityEngine.Random.Range(0, 360);

                var spawnPoint = spawnBasePosition + (Quaternion.Euler(0, randomDirection, 0) * new Vector3(1, 0, 1) * randomDistance);

                if (Physics.Raycast(spawnPoint, -spawnTransform.up, 2f, whatIsGround))
                {
                    Instantiate(model, spawnPoint, new Quaternion(0, 0, 0, 0));
                    Debug.Log($"Spawed on {spawnPoint} ");
                    nextSpawn = DateTimeOffset.Now.AddSeconds(UnityEngine.Random.Range(-10, 10) + averageSpawnTime);
                    Debug.Log($"NextSpawn in : {(nextSpawn - DateTimeOffset.Now).TotalSeconds}");
                }
                else
                {
                    Debug.Log($"Spawnlocation out of bounds.");
                }
            }
        }
    }
}

