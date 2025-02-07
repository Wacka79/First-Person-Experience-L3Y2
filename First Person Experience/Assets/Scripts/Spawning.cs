using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemyToSpawn;
    public bool hasSpawned;
    public bool isActive;

    public float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        //InvokeRepeating("Spawn", 0.1f, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSpawned == false)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if(!isActive)
        {
        StartCoroutine(SpawnTimer());
        }
        
        Instantiate(enemyToSpawn, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity); 
        hasSpawned = true;  
        
        
    }

    IEnumerator SpawnTimer()
    {
        isActive = true;
        yield return new WaitForSeconds(spawnRate);
        hasSpawned = false;
    }
}
