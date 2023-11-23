using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 3f;
    public float playerProximityThreshold = 10f; // Adjust this threshold as needed
    public Transform[] spawnPoints;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player tag is "Player"
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        if (Vector3.Distance(player.position, spawnPoint.position) > playerProximityThreshold)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
